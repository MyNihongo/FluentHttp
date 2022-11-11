using Microsoft.Extensions.Configuration;

namespace MyNihongo.FluentHttp;

internal static class ConfigurationEx
{
	public static string CreateAbsoluteUrl(this IConfiguration @this, Uri relative)
	{
		var baseUrl = @this[$"{ConfigKeys.Section}:{ConfigKeys.BaseAddress}"];
		if (string.IsNullOrEmpty(baseUrl))
			return string.Empty;

		var uri = new Uri(baseUrl, UriKind.Absolute);
		uri = new Uri(uri, relative);

		return uri.AbsoluteUri;
	}
}
