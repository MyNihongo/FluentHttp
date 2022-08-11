namespace MyNihongo.FluentHttp.Tests.Integration.FluentHttpTests;

[UsesVerify]
public sealed class SetBaseAddressShould : FluentHttpTestsBase
{
	public SetBaseAddressShould()
		: base(false)
	{
	}

	[Fact]
	public async Task GetFromBaseUri()
	{
		var options = new HttpCallOptions
		{
			BaseAddress = "https://jsonplaceholder.typicode.com/users/1"
		};

		var result = await CreateFixture()
			.GetJsonAsync(options, UserRecordContext.Default.UserRecord)
			.ToJsonStringAsync();

		await Verify(result);
	}

	[Fact]
	public async Task GetFromBaseUriAndPaths()
	{
		var options = new HttpCallOptions
		{
			BaseAddress = "https://jsonplaceholder.typicode.com",
			PathSegments = { "users", "1" }
		};

		var result = await CreateFixture()
			.GetJsonAsync(options, UserRecordContext.Default.UserRecord)
			.ToJsonStringAsync();

		await Verify(result);
	}
}