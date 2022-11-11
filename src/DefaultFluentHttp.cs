using System.Net.Http.Headers;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MyNihongo.FluentHttp;

internal sealed class DefaultFluentHttp : IFluentHttp
{
	private readonly ILogger<IFluentHttp> _logger;
	private readonly IHttpClientFactory _factory;
	private readonly IConfiguration _configuration;

	public DefaultFluentHttp(
		ILogger<IFluentHttp> logger,
		IHttpClientFactory factory,
		IConfiguration configuration)
	{
		_logger = logger;
		_factory = factory;
		_configuration = configuration;
	}

	public async Task<TResult> GetJsonAsync<TResult>(HttpCallOptions options, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default)
	{
		using var req = CreateRequest(HttpMethod.Get, options);

		return await GetResponseAsync(req, resultTypeInfo, ct)
			.ConfigureAwait(false);
	}

	public async Task<TResult?> GetJsonOrDefaultAsync<TResult>(HttpCallOptions options, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default)
	{
		using var req = CreateRequest(HttpMethod.Get, options);

		return await GetResponseOrDefaultAsync(req, resultTypeInfo, ct)
			.ConfigureAwait(false);
	}

	public async Task<TResult> PostJsonAsync<TSource, TResult>(TSource source, HttpCallOptions options, JsonTypeInfo<TSource>? sourceTypeInfo = null, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default)
	{
		using var req = await CreateRequestAsync(HttpMethod.Post, options, source, sourceTypeInfo, ct)
			.ConfigureAwait(false);

		return await GetResponseAsync(req, resultTypeInfo, ct)
			.ConfigureAwait(false);
	}

	public async Task<TResult?> PostJsonOrDefaultAsync<TSource, TResult>(TSource source, HttpCallOptions options, JsonTypeInfo<TSource>? sourceTypeInfo = null, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default)
	{
		using var req = await CreateRequestAsync(HttpMethod.Post, options, source, sourceTypeInfo, ct)
			.ConfigureAwait(false);

		return await GetResponseOrDefaultAsync(req, resultTypeInfo, ct)
			.ConfigureAwait(false);
	}

	private HttpRequestMessage CreateRequest(HttpMethod method, HttpCallOptions options)
	{
		var uri = options.CreateUri();

		if (_logger.IsEnabled(LogLevel.Trace))
		{
			var absoluteUrl = !uri.IsAbsoluteUri
				? _configuration.CreateAbsoluteUrl(uri)
				: uri.AbsoluteUri;

			_logger.LogTrace("-REQUEST-\nMethod: {Method}\nURL: {Url}", method, absoluteUrl);
		}

		return CreateRequest(method, uri, options);
	}

	private async Task<HttpRequestMessage> CreateRequestAsync<T>(HttpMethod method, HttpCallOptions options, T data, JsonTypeInfo<T>? jsonTypeInfo, CancellationToken ct)
	{
		var uri = options.CreateUri();

		HttpContent content;
		if (_logger.IsEnabled(LogLevel.Trace))
		{
			var stringData = data.Serialize();
			content = new StringContent(stringData);

			var absoluteUrl = _configuration.CreateAbsoluteUrl(uri);
			_logger.LogTrace("-REQUEST-\nMethod: {Method}\nURL: {Url}\nContent: {Content}", method, absoluteUrl, stringData);
		}
		else
		{
			// Do not dispose
			var stream = await data.SerializeAsync(jsonTypeInfo, ct)
				.ConfigureAwait(false);

			content = new StreamContent(stream);
		}

		var req = CreateRequest(method, uri, options);
		req.Content = content;
		req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json")
		{
			CharSet = Encoding.UTF8.WebName
		};

		return req;
	}

	private static HttpRequestMessage CreateRequest(in HttpMethod method, in Uri uri, in HttpCallOptions options)
	{
		var req = new HttpRequestMessage(method, uri);

		foreach (var (key, value) in options.Headers)
			req.Headers.TryAddWithoutValidation(key, value);

		return req;
	}

	private async Task<T> GetResponseAsync<T>(HttpRequestMessage req, JsonTypeInfo<T>? jsonTypeInfo, CancellationToken ct)
	{
		// Do not dispose
		var httpClient = _factory.CreateClient(Const.FactoryName);
		var url = httpClient.BaseAddress.GetAbsoluteUri(req.RequestUri);

		var startTime = DateTime.Now;

		using var res = await httpClient.SendAsync(req, ct)
			.ConfigureAwait(false);

		_logger.LogDebug("{CodeName} ({Code:D}). Request time: {RequestTime}", res.StatusCode, res.StatusCode, DateTime.Now - startTime);

		await using var stream = await res.Content
#if NETSTANDARD
			.ReadAsStreamAsync()
#else
			.ReadAsStreamAsync(ct)
#endif
			.ConfigureAwait(false);

		if (res.IsSuccessStatusCode)
		{
			if (_logger.IsEnabled(LogLevel.Trace))
			{
				var stringData = await stream.ReadToEndAsync()
					.ConfigureAwait(false);

				_logger.LogTrace("-RESPONSE-\nURL: {Url}\nContent: {Content}", url, stringData);

				return stringData.Deserialize(jsonTypeInfo);
			}

			return await stream.DeserializeAsync(jsonTypeInfo, ct)
				.ConfigureAwait(false);
		}

		var errorContent = string.Empty;

		if (stream.Length != 0)
		{
			errorContent = await stream.ReadToEndAsync()
				.ConfigureAwait(false);

			_logger.LogTrace("-RESPONSE ERROR-\nURL: {Url}\nContent: {Content}", url, errorContent);
		}

		throw new HttpCallException(res.StatusCode, errorContent);
	}

	private async Task<T?> GetResponseOrDefaultAsync<T>(HttpRequestMessage req, JsonTypeInfo<T>? jsonTypeInfo, CancellationToken ct)
	{
		try
		{
			return await GetResponseAsync(req, jsonTypeInfo, ct)
				.ConfigureAwait(false);
		}
		catch (JsonException e)
		{
			_logger.LogWarning("Cannot deserialize JSON: {Message}", e.Message);
			return default;
		}
	}
}
