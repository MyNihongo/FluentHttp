﻿using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.ObjectPool;

namespace MyNihongo.FluentHttp;

internal static class StringEx
{
	internal static readonly ObjectPool<StringBuilder> StringBuilderPool = new DefaultObjectPoolProvider()
		.CreateStringBuilderPool();

	public static StringBuilder Join(this IEnumerable<string> @this, char separator) =>
		StringBuilderPool.Get()
			.AppendJoin(separator, @this);

	public static StringBuilder Join(this string @this, IReadOnlyList<string> values, char separator)
	{
		// TODO: avoid linq-method allocation
		return StringBuilderPool.Get()
			.AppendJoin(separator, values.Prepend(@this));
	}

	public static bool ToBool(this string? @this) =>
		"true".Equals(@this, StringComparison.OrdinalIgnoreCase);

	public static T Deserialize<T>(this string @this, JsonTypeInfo<T>? jsonTypeInfo, JsonSerializerOptions? jsonOptions)
	{
		var obj = jsonTypeInfo != null
			? JsonSerializer.Deserialize(@this, jsonTypeInfo)
			: JsonSerializer.Deserialize<T>(@this, jsonOptions);

		return obj ?? throw new NullReferenceException("Cannot deserialize");
	}
}
