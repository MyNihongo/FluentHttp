﻿using System.Net;
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

				var baseAddress = configuration[ConfigKeys.BaseAddress];
				if (!string.IsNullOrEmpty(baseAddress))
					http.BaseAddress = new Uri(baseAddress, UriKind.Absolute);

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
}