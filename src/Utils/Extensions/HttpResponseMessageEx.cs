namespace MyNihongo.FluentHttp;

internal static class HttpResponseMessageEx
{
	public static string GetFileName(this HttpResponseMessage @this, string? defaultName)
	{
		if (!string.IsNullOrEmpty(defaultName))
			return defaultName;
	}
}
