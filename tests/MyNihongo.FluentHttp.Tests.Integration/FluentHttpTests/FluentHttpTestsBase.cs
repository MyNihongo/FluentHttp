using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;

namespace MyNihongo.FluentHttp.Tests.Integration.FluentHttpTests;

public abstract class FluentHttpTestsBase
{
	private readonly IServiceProvider _serviceProvider;
	private readonly LoggingLevelSwitch _loggingLevelSwitch = new(LogEventLevel.Verbose);

	protected FluentHttpTestsBase(string? env = null)
	{
		if (!string.IsNullOrEmpty(env))
			env += '.';

		var serilogLogger = new LoggerConfiguration()
			.Enrich.FromLogContext()
			.MinimumLevel.ControlledBy(_loggingLevelSwitch)
			.WriteTo.Debug()
			.CreateLogger();

		var configuration = new ConfigurationBuilder()
			.SetBasePath(AppContext.BaseDirectory)
			.AddJsonFile($"appsettings.{env}json")
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

	protected IFluentHttp CreateFixture() =>
		_serviceProvider.GetRequiredService<IFluentHttp>();
}
