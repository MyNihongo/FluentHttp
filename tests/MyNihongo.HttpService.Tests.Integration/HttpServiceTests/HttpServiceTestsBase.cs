using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MyNihongo.HttpService.Tests.Integration.HttpServiceTests;

public abstract class HttpServiceTestsBase
{
	private readonly IServiceProvider _serviceProvider;

	protected HttpServiceTestsBase()
	{
		var serilogLogger = new LoggerConfiguration()
			.Enrich.FromLogContext()
			.MinimumLevel.Verbose()
			.WriteTo.Debug()
			.CreateLogger();

		var configuration = new ConfigurationBuilder()
			.SetBasePath(AppContext.BaseDirectory)
			.AddJsonFile("appsettings.json")
			.Build();

		_serviceProvider = new ServiceCollection()
			.AddLogging(x => x.AddSerilog(serilogLogger))
			.AddSingleton<IConfiguration>(configuration)
			.AddHttpService()
			.BuildServiceProvider(true);
	}

	protected IHttpService CreateFixture() =>
		_serviceProvider.GetRequiredService<IHttpService>();
}