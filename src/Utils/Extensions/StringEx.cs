using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.ObjectPool;

namespace MyNihongo.FluentHttp;

internal static class StringEx
{
	private static readonly ObjectPool<StringBuilder> StringBuilderPool = new DefaultObjectPoolProvider()
		.CreateStringBuilderPool();

	public static string Join(this IEnumerable<string> @this, char separator) =>
		StringBuilderPool.Get()
			.AppendJoin(separator, @this)
			.ToString();

	public static bool ToBool(this string? @this) =>
		"true".Equals(@this, StringComparison.OrdinalIgnoreCase);

	public static T Deserialize<T>(this string @this, JsonTypeInfo<T>? jsonTypeInfo = null)
	{
		var obj = jsonTypeInfo != null
			? JsonSerializer.Deserialize(@this, jsonTypeInfo)
			: JsonSerializer.Deserialize<T>(@this);

		return obj ?? throw new NullReferenceException("Cannot deserialize");
	}
}
