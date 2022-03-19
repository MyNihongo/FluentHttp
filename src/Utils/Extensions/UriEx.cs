namespace MyNihongo.FluentHttp;

internal static class UriEx
{
	public static string GetAbsoluteUri(this Uri? httpClientUri, Uri? httpRequestUri)
	{
		if (httpRequestUri != null && httpRequestUri.IsAbsoluteUri)
			return httpRequestUri.AbsoluteUri;

		if (httpClientUri == null)
			throw new InvalidOperationException("Base uri is not set for the HTTP client");

		httpClientUri = httpRequestUri != null
			? new Uri(httpClientUri, httpRequestUri)
			: httpClientUri;

		return httpClientUri.AbsoluteUri;
	}
}
