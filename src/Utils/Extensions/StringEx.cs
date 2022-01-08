using System.Text;
using Microsoft.Extensions.ObjectPool;

namespace MyNihongo.HttpService;

internal static class StringEx
{
	private static readonly ObjectPool<StringBuilder> StringBuilderPool = new DefaultObjectPoolProvider()
		.CreateStringBuilderPool();

	public static string Join(this string[] @this, char separator) =>
		StringBuilderPool.Get()
			.AppendJoin(separator, @this)
			.ToString();

	public static bool ToBool(this string? @this) =>
		"true".Equals(@this, StringComparison.OrdinalIgnoreCase);
}