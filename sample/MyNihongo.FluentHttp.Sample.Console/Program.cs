using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyNihongo.FluentHttp;
using Serilog;

var currentDirectory = AppContext.BaseDirectory;
Directory.SetCurrentDirectory(currentDirectory);

var configuration = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json")
	.Build();

var serilogLogger = new LoggerConfiguration()
	.Enrich.FromLogContext()
	.MinimumLevel.Verbose()
	.WriteTo.Console()
	.CreateLogger();

var services = new ServiceCollection()
	.AddSingleton<IConfiguration>(configuration)
	.AddLogging(x => x.AddSerilog(serilogLogger))
	.AddFluentHttp()
	.BuildServiceProvider(true);

var fluentHttp = services.GetRequiredService<IFluentHttp>();

// GET 200

// POST 200

// POST non-200