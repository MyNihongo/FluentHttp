using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;

namespace MyNihongo.FluentHttp.Tests.Integration.FluentHttpTests;

public abstract class FluentHttpTestsBase
{
	private readonly IServiceProvider _serviceProvider;
	private readonly LoggingLevelSwitch _loggingLevelSwitch = new(LogEventLevel.Verbose);

	protected FluentHttpTestsBase(ITestOutputHelper testOutputHelper,  bool loadJsonConfiguration = true)
	{
		var serilogLogger = new LoggerConfiguration()
			.Enrich.FromLogContext()
			.MinimumLevel.ControlledBy(_loggingLevelSwitch)
			.MinimumLevel.Override("System", LogEventLevel.Warning)
			.WriteTo.Debug()
			.WriteTo.TestOutput(testOutputHelper)
			.CreateLogger();

		var configuration = GetConfiguration(loadJsonConfiguration);

		_serviceProvider = new ServiceCollection()
			.AddLogging(x => x.AddSerilog(serilogLogger))
			.AddSingleton(configuration)
			.AddFluentHttp()
			.BuildServiceProvider(true);
	}

	protected LogEventLevel LogLevel
	{
		set => _loggingLevelSwitch.MinimumLevel = value;
	}

	protected IFluentHttp CreateFixture() =>
		_serviceProvider.GetRequiredService<IFluentHttp>();

	private static IConfiguration GetConfiguration(bool loadJsonConfiguration)
	{
		var configurationBuilder = new ConfigurationBuilder()
			.SetBasePath(AppContext.BaseDirectory);

		if (loadJsonConfiguration)
			configurationBuilder = configurationBuilder
				.AddJsonFile("appsettings.json");

		return configurationBuilder.Build();
	}
}
