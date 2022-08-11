namespace MyNihongo.FluentHttp;

internal static class HttpCallOptionsEx
{
	public static Uri CreateUri(this HttpCallOptions @this)
	{
		return !string.IsNullOrEmpty(@this.BaseAddress)
			? new Uri(@this.BaseAddress.Join(@this.PathSegments, Const.UriSeparator), UriKind.Absolute)
			: new Uri(@this.PathSegments.Join(Const.UriSeparator), UriKind.Relative);
	}
}
