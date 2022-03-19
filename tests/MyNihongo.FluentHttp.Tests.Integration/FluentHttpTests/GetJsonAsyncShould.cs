namespace MyNihongo.FluentHttp.Tests.Integration.FluentHttpTests;

[UsesVerify]
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
			.GetJsonAsync(options, UserRecordContext.Default.UserRecordArray)
			.ToJsonStringAsync();

		await Verify(result);
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
			.GetJsonAsync(options, UserRecordContext.Default.UserRecordArray)
			.ToJsonStringAsync();

		await Verify(result);
	}

	[Fact]
	public async Task GetInvalidModel()
	{
		var options = new HttpCallOptions
		{
			PathSegments = { "users" }
		};

		var result = await CreateFixture()
			.GetJsonOrDefaultAsync(options, PostRecordContext.Default.PostRecord);

		result
			.Should()
			.BeNull();
	}
}
