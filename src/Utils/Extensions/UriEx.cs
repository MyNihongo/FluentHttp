namespace MyNihongo.FluentHttp;

internal static class UriEx
{
	public static string GetAbsoluteUri(this Uri? httpClientUri, Uri? httpRequestUri)
	{
		if (httpRequestUri != null && httpRequestUri.IsAbsoluteUri)
			return httpRequestUri.AbsoluteUri;

		if (httpClientUri == null)
			throw new InvalidOperationException("Base uri is not set for the HTTP client");

		if (httpRequestUri != null)
		{
			httpClientUri = httpRequestUri.OriginalString.StartsWith(Const.UriSeparator)
				? new Uri(httpClientUri, httpRequestUri.OriginalString[1..])
				: new Uri(httpClientUri, httpRequestUri);
		}

		return httpClientUri.AbsoluteUri;
	}
}
