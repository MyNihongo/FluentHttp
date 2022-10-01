namespace MyNihongo.FluentHttp;

internal static class HttpCallOptionsEx
{
	public static Uri CreateUri(this HttpCallOptions @this)
	{
		UriKind uriKind;
		string uriString;

		if (string.IsNullOrEmpty(@this.BaseAddress))
		{
			uriString = @this.PathSegments.Join(Const.UriSeparator);
			uriKind = UriKind.Relative;
		}
		else
		{
			uriString = @this.BaseAddress.Join(@this.PathSegments, Const.UriSeparator);
			uriKind = UriKind.Absolute;
		}

		return new Uri(uriString, uriKind);
	}
}
