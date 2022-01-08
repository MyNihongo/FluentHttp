using System.Text.Json.Serialization.Metadata;

namespace MyNihongo.HttpService;

public static class HttpServiceWithOptionsEx
{
	/// <inheritdoc cref="IHttpService.GetJsonAsync{T}"/>
	public static Task<TResult> GetJsonAsync<TResult>(this IHttpServiceWithOptions @this, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default) =>
		@this.HttpService.GetJsonAsync(@this.Options, resultTypeInfo, ct);

	/// <inheritdoc cref="IHttpService.PostJsonAsync{T,T}"/>
	public static Task<TResult> PostJsonAsync<TSource, TResult>(this IHttpServiceWithOptions @this, TSource source, JsonTypeInfo<TSource>? sourceTypeInfo = null, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default) =>
		@this.HttpService.PostJsonAsync(source, @this.Options, sourceTypeInfo, resultTypeInfo, ct);

	/// <inheritdoc cref="HttpServiceEx.AppendPathSegment"/>
	public static IHttpServiceWithOptions AppendPathSegment(this IHttpServiceWithOptions @this, string pathSegment)
	{
		@this.Options.PathSegments.Add(pathSegment);
		return @this;
	}

	/// <inheritdoc cref="HttpServiceEx.AppendPathSegments"/>
	public static IHttpServiceWithOptions AppendPathSegments(this IHttpServiceWithOptions @this, params string[] pathSegments)
	{
		@this.Options.PathSegments.AddRange(pathSegments);
		return @this;
	}

	/// <inheritdoc cref="HttpServiceEx.WithHeader"/>
	public static IHttpServiceWithOptions WithHeader(this IHttpServiceWithOptions @this, string header, string value)
	{
		@this.Options.Headers.Add(header, value);
		return @this;
	}
}