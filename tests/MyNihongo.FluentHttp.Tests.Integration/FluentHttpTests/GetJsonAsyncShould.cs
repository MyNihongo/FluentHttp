namespace MyNihongo.FluentHttp.Tests.Integration.FluentHttpTests;

public sealed class GetJsonAsyncShould : FluentHttpTestsBase
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