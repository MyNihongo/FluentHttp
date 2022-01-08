using System.Text.Json.Serialization.Metadata;

namespace MyNihongo.HttpService;

public interface IHttpService
{
	/// <exception cref="HttpCallException"></exception>
	Task<TResult> GetJsonAsync<TResult>(HttpCallOptions options, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default);

	/// <exception cref="HttpCallException"></exception>
	Task<TResult> PostJsonAsync<TSource, TResult>(TSource source, HttpCallOptions options, JsonTypeInfo<TSource>? sourceTypeInfo = null, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default);
}