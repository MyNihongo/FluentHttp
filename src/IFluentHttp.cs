using System.Text.Json.Serialization.Metadata;

namespace MyNihongo.FluentHttp;

public interface IFluentHttp
{
	/// <exception cref="HttpCallException"></exception>
	/// <exception cref="JsonException"></exception>
	Task<TResult> GetJsonAsync<TResult>(HttpCallOptions options, JsonTypeInfo<TResult>? resultTypeInfo = null, JsonSerializerOptions? jsonOptions = null, CancellationToken ct = default);

	/// <summary>
	///	Attempts to GET a JSON value. Returns <see langword="null"/> in case the returned model is not a valid JSON.
	/// </summary>
	/// <exception cref="HttpCallException"></exception>
	Task<TResult?> GetJsonOrDefaultAsync<TResult>(HttpCallOptions options, JsonTypeInfo<TResult>? resultTypeInfo = null, JsonSerializerOptions? jsonOptions = null, CancellationToken ct = default);

	/// <exception cref="HttpCallException"></exception>
	/// <exception cref="JsonException"></exception>
	Task<TResult> PostJsonAsync<TSource, TResult>(TSource source, HttpCallOptions options, JsonTypeInfo<TSource>? sourceTypeInfo = null, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default);

	/// <summary>
	///	Attempts to POST a JSON value. Returns <see langword="null"/> in case the returned model is not a valid JSON.
	/// </summary>
	/// <exception cref="HttpCallException"></exception>
	Task<TResult?> PostJsonOrDefaultAsync<TSource, TResult>(TSource source, HttpCallOptions options, JsonTypeInfo<TSource>? sourceTypeInfo = null, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default);

	/// <exception cref="HttpCallException"></exception>
	/// <exception cref="JsonException"></exception>
	Task<TResult> PostJsonAsync<TSource, TResult>(TSource source, HttpCallOptions options, JsonSerializerOptions jsonOptions, CancellationToken ct = default);

	/// <summary>
	///	Attempts to POST a JSON value. Returns <see langword="null"/> in case the returned model is not a valid JSON.
	/// </summary>
	/// <exception cref="HttpCallException"></exception>
	Task<TResult?> PostJsonOrDefaultAsync<TSource, TResult>(TSource source, HttpCallOptions options, JsonSerializerOptions jsonOptions, CancellationToken ct = default);

	/// <summary>
	/// Downloads a file to <see cref="localFolderPath"/>
	/// </summary>
	/// <returns>Full path to the downloaded file</returns>
	Task<string> DownloadFileAsync(HttpCallOptions options, string localFolderPath, string? localFileName = null, CancellationToken ct = default);

	/// <exception cref="HttpCallException"></exception>
	/// <exception cref="JsonException"></exception>
	Task<TResult> SendJsonAsync<TResult>(HttpMethod httpMethod, HttpCallOptions options, object? source, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default);

	/// <exception cref="HttpCallException"></exception>
	/// <exception cref="JsonException"></exception>
	Task<TResult> SendJsonAsync<TResult>(HttpMethod httpMethod, HttpCallOptions options, object? source, JsonSerializerOptions jsonOptions, CancellationToken ct = default);
}
