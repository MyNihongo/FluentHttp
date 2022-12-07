using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyNihongo.FluentHttp;
using MyNihongo.FluentHttp.Sample.Console;
using Serilog;
using Serilog.Events;

var currentDirectory = AppContext.BaseDirectory;
Directory.SetCurrentDirectory(currentDirectory);

var configuration = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json")
	.Build();

var serilogLogger = new LoggerConfiguration()
	.Enrich.FromLogContext()
	.MinimumLevel.Verbose()
	.MinimumLevel.Override("System", LogEventLevel.Warning)
	.WriteTo.Console()
	.CreateLogger();

var services = new ServiceCollection()
	.AddSingleton<IConfiguration>(configuration)
	.AddLogging(x => x.AddSerilog(serilogLogger))
	.AddFluentHttp()
	.BuildServiceProvider(true);

var fluentHttp = services.GetRequiredService<IFluentHttp>();

// GET 200
await fluentHttp
	.AppendPathSegment("users")
	.GetJsonAsync(UserRecordContext.Default.UserRecordArray)
	.ConfigureAwait(false);

// POST 200
var postData = new PostCreateRecord
{
	UserId = 2,
	Title = "Kanji",
	Body = "I love kanji more than katakana"
};

await fluentHttp
	.AppendPathSegment("posts")
	.PostJsonAsync(postData, PostCreateRecordContext.Default.PostCreateRecord, PostRecordContext.Default.PostRecord)
	.ConfigureAwait(false);
