using System.Text.Json;

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

	[Fact]
	public async Task GetWithJsonOptions()
	{
		var options = new HttpCallOptions
		{
			PathSegments = { "users" }
		};

		var jsonOptions = new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};

		var result = await CreateFixture()
			.GetJsonAsync<UserRecord[]>(options, jsonOptions: jsonOptions)
			.ToJsonStringAsync();

		await Verify(result);
	}
}
