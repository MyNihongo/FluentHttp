namespace MyNihongo.FluentHttp.Tests.Unit.FluentHttpTests;

public sealed class WithHeaderShould : FluentHttpTestsBase
{
	[Fact]
	public async Task AddSingleHeader()
	{
		const string header = nameof(header), value = nameof(value);

		var expectedOptions = new HttpCallOptions
		{
			Headers = { { header, value } }
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.WithHeader(header, value)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}

	[Fact]
	public async Task AddSingleHeaderForOptions()
	{
		const string header = nameof(header), value = nameof(value),
			pathSegment = nameof(pathSegment);

		var expectedOptions = new HttpCallOptions
		{
			Headers = { { header, value } },
			PathSegments = { pathSegment }
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.AppendPathSegment(pathSegment)
			.WithHeader(header, value)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}

	[Fact]
	public async Task AddMultipleHeaders()
	{
		const string header1 = nameof(header1), value1 = nameof(value1),
			header2 = nameof(header2), value2 = nameof(value2);

		var expectedOptions = new HttpCallOptions
		{
			Headers = { { header1, value1 }, { header2, value2 } }
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.WithHeader(header1, value1)
			.WithHeader(header2, value2)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}
}