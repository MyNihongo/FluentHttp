﻿using System.Text.Json.Serialization.Metadata;

namespace MyNihongo.FluentHttp;

public interface IFluentHttp
{
	/// <exception cref="HttpCallException"></exception>
	Task<TResult> GetJsonAsync<TResult>(HttpCallOptions options, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default);

	/// <exception cref="HttpCallException"></exception>
	Task<TResult> PostJsonAsync<TSource, TResult>(TSource source, HttpCallOptions options, JsonTypeInfo<TSource>? sourceTypeInfo = null, JsonTypeInfo<TResult>? resultTypeInfo = null, CancellationToken ct = default);
}