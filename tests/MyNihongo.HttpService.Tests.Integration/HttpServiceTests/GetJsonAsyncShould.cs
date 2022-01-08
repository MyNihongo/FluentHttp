using System.Threading.Tasks;
using MyNihongo.HttpService.Tests.Integration.Models;
using Serilog.Events;
using Xunit;

namespace MyNihongo.HttpService.Tests.Integration.HttpServiceTests;

public sealed class GetJsonAsyncShould : HttpServiceTestsBase
{
	[Fact]
	public async Task GetDataWithVerboseLogging()
	{
		var options = new HttpCallOptions
		{
			PathSegments = { "users" }
		};

		var result = await CreateFixture()
			.GetJsonAsync(options, UserRecordContext.Default.UserRecordArray);

		ApprovalTests.VerifyJson(result);
	}

	[Fact]
	public async Task GetDataWithoutVerboseLogging()
	{
		LogLevel = LogEventLevel.Information;

		var options = new HttpCallOptions
		{
			PathSegments = { "users" }
		};

		var result = await CreateFixture()
			.GetJsonAsync(options, UserRecordContext.Default.UserRecordArray);

		ApprovalTests.VerifyJson(result);
	}
}