using System.Text.Json;

namespace MyNihongo.FluentHttp.Tests.Integration.FluentHttpTests;

[UsesVerify]
public sealed class GetJsonAsyncShould : FluentHttpTestsBase
{
	public GetJsonAsyncShould(ITestOutputHelper testOutputHelper)
		: base(testOutputHelper)
	{
	}
	
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
	
	[Fact]
	public async Task ThrowIfNotFound()
	{
		var jsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};
		
		var action = async () => await CreateFixture()
			.AppendPathSegment("not-exists")
			.GetJsonAsync<UserRecord>(jsonOptions: jsonOptions);

		await action
			.Should()
			.ThrowExactlyAsync<HttpCallException>();
	}
	
	[Fact]
	public async Task ReturnNullIfNotFoundWithTryGet()
	{
		var jsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};

		var result = await CreateFixture()
			.AppendPathSegment("not-exists")
			.GetJsonOrDefaultAsync<UserRecord>(jsonOptions: jsonOptions);

		result
			.Should()
			.BeNull();
	}
}
