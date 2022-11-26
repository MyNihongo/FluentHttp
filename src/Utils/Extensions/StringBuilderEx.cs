namespace MyNihongo.FluentHttp;

internal static class StringBuilderEx
{
	public static string ToStringAndReturn(this StringBuilder @this)
	{
		var value = @this.ToString();
		StringEx.StringBuilderPool.Return(@this);

		return value;
	}
}
