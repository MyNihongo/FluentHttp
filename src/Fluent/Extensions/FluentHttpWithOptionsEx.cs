using System.Text.Json.Serialization.Metadata;

namespace MyNihongo.FluentHttp;

public static class FluentHttpWithOptionsEx
{
	/// <inheritdoc cref="IFluentHttp.GetJsonAsync{T}(HttpCallOptions, JsonTypeInfo{T}?, JsonSerializerOptions?, CancellationToken)"/>
	public static Task<TResult> GetJsonAsync<TResult>(this IFluentHttpWithOptions @this, JsonTypeInfo<TResult>? resultTypeInfo = null, JsonSerializerOptions? jsonOptions = null, CancellationToken ct = default) =>
		@this.Http.GetJsonAsync(@this.Options, resultTypeInfo, jsonOptions, ct);

	/// <inheritdoc cref="IFluentHttp.GetJsonOrDefaultAsync{T}(HttpCallOptions, JsonTypeInfo{T}?, JsonSerializerOptions?, CancellationToken)"/>
	public static Task<TResult?> GetJsonOrDefaultAsync<TResult>(this IFluentHttpWithOptions @this, JsonTypeInfo<TResult>? resultTypeInfo = null, JsonSerializerOptions? jsonOptions = null, CancellationToken ct = default) =>
		@this.Http.GetJsonOrDefaultAsync(@this.Options, resultTypeInfo, jsonOptions, ct);

	/// <inheritdoc cref="IFluentHttp.PostJsonAsync{T,TT}(T, HttpCallOptions, JsonTypeInfo{T}?, JsonTypeInfo{TT}?, CancellationToken)"/>
	public static Task<TResult> PostJsonAsync<TSource, TResult>(this IFluentHttpWithOptions @this, TSource source, JsonTypeInfo<TSource>? sourceTypeInfo = null, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default) =>
		@this.Http.PostJsonAsync(source, @this.Options, sourceTypeInfo, resultTypeInfo, ct);

	/// <inheritdoc cref="IFluentHttp.PostJsonOrDefaultAsync{T,TT}(T, HttpCallOptions, JsonTypeInfo{T}?, JsonTypeInfo{TT}?, CancellationToken)"/>
	public static Task<TResult?> PostJsonOrDefaultAsync<TSource, TResult>(this IFluentHttpWithOptions @this, TSource source, JsonTypeInfo<TSource>? sourceTypeInfo = null, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default) =>
		@this.Http.PostJsonOrDefaultAsync(source, @this.Options, sourceTypeInfo, resultTypeInfo, ct);
	
	/// <inheritdoc cref="IFluentHttp.PostJsonAsync{T,TT}(T, HttpCallOptions, JsonSerializerOptions, CancellationToken)"/>
	public static Task<TResult> PostJsonAsync<TSource, TResult>(this IFluentHttpWithOptions @this, TSource source, JsonSerializerOptions jsonOptions, CancellationToken ct = default) =>
		@this.Http.PostJsonAsync<TSource, TResult>(source, @this.Options, jsonOptions, ct);

	/// <inheritdoc cref="IFluentHttp.PostJsonOrDefaultAsync{T,TT}(T, HttpCallOptions, JsonSerializerOptions, CancellationToken)"/>
	public static Task<TResult?> PostJsonOrDefaultAsync<TSource, TResult>(this IFluentHttpWithOptions @this, TSource source, JsonSerializerOptions jsonOptions, CancellationToken ct = default) =>
		@this.Http.PostJsonOrDefaultAsync<TSource, TResult>(source, @this.Options, jsonOptions, ct);

	/// <inheritdoc cref="IFluentHttp.DownloadFileAsync"/> 
	public static Task<string> DownloadFileAsync(this IFluentHttpWithOptions @this, string localFolderPath, string? localFileName = null, CancellationToken ct = default) =>
		@this.Http.DownloadFileAsync(@this.Options, localFolderPath, localFileName, ct);
	
	/// <inheritdoc cref="IFluentHttp.SendJsonAsync{T}(HttpMethod, HttpCallOptions, object, JsonTypeInfo{T}?, CancellationToken)"/> 
	public static Task<TResult> SendJsonAsync<TResult>(this IFluentHttpWithOptions @this, HttpMethod httpMethod, object? source, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default) =>
		@this.Http.SendJsonAsync(httpMethod, @this.Options, source, resultTypeInfo, ct);
	
	/// <inheritdoc cref="IFluentHttp.SendJsonAsync{T}(HttpMethod, HttpCallOptions, object, JsonSerializerOptions, CancellationToken)"/> 
	public static Task<TResult> SendJsonAsync<TResult>(this IFluentHttpWithOptions @this, HttpMethod httpMethod, object? source, JsonSerializerOptions jsonOptions, CancellationToken ct = default) =>
		@this.Http.SendJsonAsync<TResult>(httpMethod, @this.Options, source, jsonOptions, ct);

	/// <inheritdoc cref="FluentHttpEx.SetOptions"/>
	public static IFluentHttpWithOptions SetOptions(this IFluentHttpWithOptions @this, Action<HttpCallOptions> options)
	{
		options(@this.Options);
		return @this;
	}

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

	/// <inheritdoc cref="FluentHttpEx.AppendParameter"/>
	public static IFluentHttpWithOptions AppendParameter(this IFluentHttpWithOptions @this, string key, object? value)
	{
		var strValue = value?.ToString();

		if (strValue != null)
			@this.Options.Parameters.Add(key, strValue);

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
