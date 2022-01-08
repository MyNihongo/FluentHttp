using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace MyNihongo.FluentHttp.Tests.Integration.HttpServiceTests;

public abstract class HttpServiceTestsBase
{
	private readonly IServiceProvider _serviceProvider;
	private readonly LoggingLevelSwitch _loggingLevelSwitch = new(LogEventLevel.Verbose);

	protected HttpServiceTestsBase()
	{
		var serilogLogger = new LoggerConfiguration()
			.Enrich.FromLogContext()
			.MinimumLevel.ControlledBy(_loggingLevelSwitch)
			.WriteTo.Debug()
			.CreateLogger();

		var configuration = new ConfigurationBuilder()
			.SetBasePath(AppContext.BaseDirectory)
			.AddJsonFile("appsettings.json")
			.Build();

		_serviceProvider = new ServiceCollection()
			.AddLogging(x => x.AddSerilog(serilogLogger))
			.AddSingleton<IConfiguration>(configuration)
			.AddFluentHttp()
			.BuildServiceProvider(true);
	}

	protected LogEventLevel LogLevel
	{
		set => _loggingLevelSwitch.MinimumLevel = value;
	}

	protected IHttpService CreateFixture() =>
		_serviceProvider.GetRequiredService<IHttpService>();
}