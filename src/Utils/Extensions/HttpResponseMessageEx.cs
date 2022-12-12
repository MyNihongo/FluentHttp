namespace MyNihongo.FluentHttp;

internal static class HttpResponseMessageEx
{
	public static Task<Stream> ReadAsStreamAsync(this HttpResponseMessage @this, CancellationToken ct)
	{
		return @this.Content
#if NETSTANDARD
			.ReadAsStreamAsync();
#else
			.ReadAsStreamAsync(ct);
#endif
	}
}
