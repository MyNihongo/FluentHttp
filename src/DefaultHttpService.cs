using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MyNihongo.HttpService;

internal sealed class DefaultHttpService : IHttpService
{
	private readonly ILogger<IHttpService> _logger;
	private readonly IHttpClientFactory _factory;
	private readonly IConfiguration _configuration;

	public DefaultHttpService(
		ILogger<IHttpService> logger,
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
	}

	public Task<TResult> PostJsonAsync<TSource, TResult>(TSource source, HttpCallOptions options, JsonTypeInfo<TSource>? sourceTypeInfo = null, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default)
	{
		throw new NotImplementedException();
	}

	private static HttpRequestMessage CreateRequest(HttpMethod method, HttpCallOptions options)
	{
		var uri = options.CreateUri();
		return new HttpRequestMessage(method, uri);
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
			_logger.LogTrace("Method: {Method}\nURL: {Url}\nContent: {Content}", method, absoluteUrl, stringData);
		}
		else
		{
			// Do not dispose
			var stream = await data.SerializeAsync(jsonTypeInfo, ct)
				.ConfigureAwait(false);

			content = new StreamContent(stream);
		}

		var request = new HttpRequestMessage(method, uri)
		{
			Content = content
		};

		request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json")
		{
			CharSet = Encoding.UTF8.WebName
		};

		return request;
	}
}