using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyNihongo.FluentHttp;

public static class ServiceCollectionEx
{
	public static IServiceCollection AddFluentHttp(this IServiceCollection @this, Action<IServiceProvider, HttpClient>? configure = null)
	{
		@this
			.AddHttpClient(Const.FactoryName)
			.ConfigureHttpClient((services, http) =>
			{
				var configuration = services.GetRequiredService<IConfiguration>()
					.GetSection(ConfigKeys.Section);

				if (configuration.TryGetBaseAddress(out var baseAddress))
					http.BaseAddress = new Uri(baseAddress, UriKind.Absolute);
				if (configuration.TryGetTimeout(out var timeout))
					http.Timeout = timeout;

				configure?.Invoke(services, http);
			})
			.ConfigurePrimaryHttpMessageHandler(static services =>
			{
				var configuration = services.GetRequiredService<IConfiguration>()
					.GetSection(ConfigKeys.Section);

				var useNtlm = configuration[ConfigKeys.UseNtlmAuthentication].ToBool();

				var handler = new HttpClientHandler();

				if (useNtlm)
					handler.Credentials = CredentialCache.DefaultNetworkCredentials;

				return handler;
			});

		return @this.AddSingleton<IFluentHttp, DefaultFluentHttp>();
	}

	private static bool TryGetBaseAddress(this IConfiguration configuration, [NotNullWhen(true)] out string? value)
	{
		value = configuration[ConfigKeys.BaseAddress];
		return !string.IsNullOrEmpty(value);
	}

	private static bool TryGetTimeout(this IConfiguration configuration, out TimeSpan value)
	{
		value = TimeSpan.Zero;
		if (!int.TryParse(configuration[ConfigKeys.Timeout], out var timeoutSeconds))
			return false;

		value = timeoutSeconds != Timeout.Infinite
			? TimeSpan.FromSeconds(timeoutSeconds)
			: Timeout.InfiniteTimeSpan;

		return true;
	}
}
