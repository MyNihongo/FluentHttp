namespace MyNihongo.FluentHttp.Tests.Unit.FluentHttpTests;

public sealed class GetJsonShould : FluentHttpTestsBase
{
	[Fact]
	public async Task GetJson()
	{
		const string pathSegment = "test";

		var expectedOptions = new HttpCallOptions
		{
			PathSegments = { pathSegment }
		};

		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.AppendPathSegment(pathSegment)
			.GetJsonAsync<ResponseRecord>(ct: cts.Token);

		VerifyGet(expectedOptions, cts.Token);
	}

	[Fact]
	public async Task GetJsonOrDefault()
	{
		const string pathSegment = "test";

		var expectedOptions = new HttpCallOptions
		{
			PathSegments = { pathSegment }
		};

		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.AppendPathSegment(pathSegment)
			.GetJsonOrDefaultAsync<ResponseRecord>(ct: cts.Token);

		VerifyGetOrDefault(expectedOptions, cts.Token);
	}
}