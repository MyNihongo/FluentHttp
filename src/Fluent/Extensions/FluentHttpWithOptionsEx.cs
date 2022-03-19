using System.Text.Json.Serialization.Metadata;

namespace MyNihongo.FluentHttp;

public static class FluentHttpWithOptionsEx
{
	/// <inheritdoc cref="IFluentHttp.GetJsonAsync{T}"/>
	public static Task<TResult> GetJsonAsync<TResult>(this IFluentHttpWithOptions @this, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default) =>
		@this.Http.GetJsonAsync(@this.Options, resultTypeInfo, ct);

	/// <inheritdoc cref="IFluentHttp.GetJsonOrDefaultAsync{T}"/>
	public static Task<TResult?> GetJsonOrDefaultAsync<TResult>(this IFluentHttpWithOptions @this, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default) =>
		@this.Http.GetJsonOrDefaultAsync(@this.Options, resultTypeInfo, ct);

	/// <inheritdoc cref="IFluentHttp.PostJsonAsync{T,T}"/>
	public static Task<TResult> PostJsonAsync<TSource, TResult>(this IFluentHttpWithOptions @this, TSource source, JsonTypeInfo<TSource>? sourceTypeInfo = null, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default) =>
		@this.Http.PostJsonAsync(source, @this.Options, sourceTypeInfo, resultTypeInfo, ct);

	/// <inheritdoc cref="IFluentHttp.PostJsonOrDefaultAsync{T,T}"/>
	public static Task<TResult?> PostJsonOrDefaultAsync<TSource, TResult>(this IFluentHttpWithOptions @this, TSource source, JsonTypeInfo<TSource>? sourceTypeInfo = null, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default) =>
		@this.Http.PostJsonOrDefaultAsync(source, @this.Options, sourceTypeInfo, resultTypeInfo, ct);

	/// <inheritdoc cref="FluentHttpEx.AppendPathSegment"/>
	public static IFluentHttpWithOptions AppendPathSegment(this IFluentHttpWithOptions @this, string pathSegment)
	{
		@this.Options.PathSegments.Add(pathSegment);
		return @this;
	}

	/// <inheritdoc cref="FluentHttpEx.AppendPathSegments"/>
	public static IFluentHttpWithOptions AppendPathSegments(this IFluentHttpWithOptions @this, params string[] pathSegments)
	{
		@this.Options.PathSegments.AddRange(pathSegments);
		return @this;
	}

	/// <inheritdoc cref="FluentHttpEx.WithHeader"/>
	public static IFluentHttpWithOptions WithHeader(this IFluentHttpWithOptions @this, string header, string value)
	{
		@this.Options.Headers.Add(header, value);
		return @this;
	}

	/// <inheritdoc cref="FluentHttpEx.WithBasicAuth"/>
	public static IFluentHttpWithOptions WithBasicAuth(this IFluentHttpWithOptions @this, string username, string password)
	{
		var (header, value) = AuthUtils.BasicAuth(username, password);
		return @this.WithHeader(header, value);
	}
}