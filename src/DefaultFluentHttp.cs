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

	public async Task<TResult> GetJsonAsync<TResult>(HttpCallOptions options, JsonTypeInfo<TResult>? resultTypeInfo = null, JsonSerializerOptions? jsonOptions = null, CancellationToken ct = default)
	{
		using var req = CreateRequest(HttpMethod.Get, options);

		return await GetJsonResponseAsync(req, resultTypeInfo, jsonOptions, ct)
			.ConfigureAwait(false);
	}

	public async Task<TResult?> GetJsonOrDefaultAsync<TResult>(HttpCallOptions options, JsonTypeInfo<TResult>? resultTypeInfo = null, JsonSerializerOptions? jsonOptions = null, CancellationToken ct = default)
	{
		using var req = CreateRequest(HttpMethod.Get, options);

		return await GetJsonResponseOrDefaultAsync(req, resultTypeInfo, jsonOptions, ct)
			.ConfigureAwait(false);
	}

	public async Task<TResult> PostJsonAsync<TSource, TResult>(TSource source, HttpCallOptions options, JsonTypeInfo<TSource>? sourceTypeInfo = null, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default)
	{
		using var req = await CreateRequestAsync(HttpMethod.Post, options, source, sourceTypeInfo, jsonOptions: null, ct)
			.ConfigureAwait(false);

		return await GetJsonResponseAsync(req, resultTypeInfo, jsonOptions: null, ct)
			.ConfigureAwait(false);
	}

	public async Task<TResult?> PostJsonOrDefaultAsync<TSource, TResult>(TSource source, HttpCallOptions options, JsonTypeInfo<TSource>? sourceTypeInfo = null, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default)
	{
		using var req = await CreateRequestAsync(HttpMethod.Post, options, source, sourceTypeInfo, jsonOptions: null, ct)
			.ConfigureAwait(false);

		return await GetJsonResponseOrDefaultAsync(req, resultTypeInfo, jsonOptions: null, ct)
			.ConfigureAwait(false);
	}

	public async Task<TResult> PostJsonAsync<TSource, TResult>(TSource source, HttpCallOptions options, JsonSerializerOptions jsonOptions, CancellationToken ct = default)
	{
		using var req = await CreateRequestAsync(HttpMethod.Post, options, source, jsonTypeInfo: null, jsonOptions, ct)
			.ConfigureAwait(false);

		return await GetJsonResponseAsync<TResult>(req, jsonTypeInfo: null, jsonOptions, ct)
			.ConfigureAwait(false);
	}

	public async Task<TResult?> PostJsonOrDefaultAsync<TSource, TResult>(TSource source, HttpCallOptions options, JsonSerializerOptions jsonOptions, CancellationToken ct = default)
	{
		using var req = await CreateRequestAsync(HttpMethod.Post, options, source, jsonTypeInfo: null, jsonOptions, ct)
			.ConfigureAwait(false);

		return await GetJsonResponseOrDefaultAsync<TResult>(req, jsonTypeInfo: null, jsonOptions, ct)
			.ConfigureAwait(false);
	}

	public async Task<string> DownloadFileAsync(HttpCallOptions options, string localFolderPath, string? localFileName = null, CancellationToken ct = default)
	{
		using var req = CreateRequest(HttpMethod.Get, options);

		return await SaveFileAsync(req, localFolderPath, localFileName, ct)
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

			_logger.LogRequest(method, absoluteUrl);
		}

		return CreateRequest(method, uri, options);
	}

	private async Task<HttpRequestMessage> CreateRequestAsync<T>(HttpMethod method, HttpCallOptions options, T data, JsonTypeInfo<T>? jsonTypeInfo, JsonSerializerOptions? jsonOptions, CancellationToken ct)
	{
		var uri = options.CreateUri();

		HttpContent content;
		if (_logger.IsEnabled(LogLevel.Trace))
		{
			var stringData = data.Serialize(jsonTypeInfo, jsonOptions);
			content = new StringContent(stringData);

			var absoluteUrl = _configuration.CreateAbsoluteUrl(uri);
			_logger.LogRequest(method, absoluteUrl, stringData);
		}
		else
		{
			// Do not dispose
			var stream = await data.SerializeAsync(jsonTypeInfo, jsonOptions, ct)
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

	private async Task<T> GetJsonResponseAsync<T>(HttpRequestMessage req, JsonTypeInfo<T>? jsonTypeInfo, JsonSerializerOptions? jsonOptions, CancellationToken ct)
	{
		using var res = await GetResponseAsync(req, ct)
			.ConfigureAwait(false);

		if (_logger.IsEnabled(LogLevel.Trace))
		{
			var stringData = await res.ReadToEndAsync(ct)
				.ConfigureAwait(false);

			_logger.LogResponse(res.Url, stringData);
			return stringData.Deserialize(jsonTypeInfo, jsonOptions);
		}

		return await res.DeserializeAsync(jsonTypeInfo, jsonOptions, ct)
			.ConfigureAwait(false);
	}

	private async Task<string> SaveFileAsync(HttpRequestMessage req, string localFolderPath, string? localFileName, CancellationToken ct)
	{
		using var res = await GetResponseAsync(req, ct)
			.ConfigureAwait(false);

		await using var resStream = await res.ReadAsStreamAsync(ct)
			.ConfigureAwait(false);

		var filePath = FileUtils.GetFileResponseFilePath(res, localFolderPath, localFileName);
		await using var fileStream = FileUtils.AsyncStream(filePath);

		await resStream.CopyToAsync(fileStream, ct)
			.ConfigureAwait(false);

		return filePath;
	}

	private async Task<UrlResponse> GetResponseAsync(HttpRequestMessage req, CancellationToken ct)
	{
		// Do not dispose
		var httpClient = _factory.CreateClient(Const.FactoryName);
		var url = httpClient.BaseAddress.GetAbsoluteUri(req.RequestUri);

		var startTime = DateTime.Now;

		// Do not dispose
		var res = await httpClient.SendAsync(req, ct)
			.ConfigureAwait(false);

		_logger.LogRequestTime(res.StatusCode, (int)res.StatusCode, DateTime.Now - startTime);

		if (res.IsSuccessStatusCode)
			return new UrlResponse(res, url);

		using (res)
		{
			var errorContent = string.Empty;
		
			await using var stream = await res.ReadAsStreamAsync(ct)
				.ConfigureAwait(false);

			if (stream.Length != 0)
			{
				errorContent = await stream.ReadToEndAsync()
					.ConfigureAwait(false);

				_logger.LogResponseError(url, errorContent);
			}

			throw new HttpCallException(res.StatusCode, errorContent);	
		}
	}

	private async Task<T?> GetJsonResponseOrDefaultAsync<T>(HttpRequestMessage req, JsonTypeInfo<T>? jsonTypeInfo, JsonSerializerOptions? jsonOptions, CancellationToken ct)
	{
		try
		{
			return await GetJsonResponseAsync(req, jsonTypeInfo, jsonOptions, ct)
				.ConfigureAwait(false);
		}
		catch (JsonException e)
		{
			_logger.LogJsonSerializationFailed(e.Message);
			return default;
		}
	}
}
