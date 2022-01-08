namespace MyNihongo.FluentHttp;

internal static class HttpCallOptionsEx
{
	public static Uri CreateUri(this HttpCallOptions @this) =>
		new(@this.PathSegments.Join(Const.UriSeparator), UriKind.Relative);
}