namespace MyNihongo.FluentHttp;

internal static class HttpCallOptionsEx
{
	public static Uri CreateUri(this HttpCallOptions @this)
	{
		var uriString = @this.PathSegments.Join(Const.UriSeparator);
		if (!string.IsNullOrEmpty(@this.TrailingUrl))
		{
			uriString += !@this.TrailingUrl.StartsWith(Const.UriSeparator)
				? Const.UriSeparator + @this.TrailingUrl
				: @this.TrailingUrl;
		}

		return new Uri(uriString, UriKind.Relative);
	}
}
