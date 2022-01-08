using Microsoft.Extensions.Configuration;

namespace MyNihongo.HttpService;

internal static class ConfigurationEx
{
	public static string CreateAbsoluteUrl(this IConfiguration @this, Uri relative)
	{
		var baseUri = @this[$"{ConfigKeys.Section}:{ConfigKeys.BaseAddress}"];

		return !string.IsNullOrEmpty(baseUri)
			? baseUri + Const.UriSeparator + relative.AbsolutePath
			: relative.AbsolutePath;
	}
}