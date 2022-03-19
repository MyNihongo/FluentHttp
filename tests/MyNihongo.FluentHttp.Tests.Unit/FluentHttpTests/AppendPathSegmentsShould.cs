namespace MyNihongo.FluentHttp.Tests.Unit.FluentHttpTests;

public sealed class AppendPathSegmentsShould : FluentHttpTestsBase
{
	[Fact]
	public async Task AppendArraySegments()
	{
		const string pathSegment1 = nameof(pathSegment1),
			pathSegment2 = nameof(pathSegment2),
			pathSegment3 = nameof(pathSegment3);

		var expectedOptions = new HttpCallOptions
		{
			PathSegments = { pathSegment1, pathSegment2, pathSegment3 }
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.AppendPathSegments(pathSegment1, pathSegment2, pathSegment3)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}

	[Fact]
	public async Task AppendArraySegmentsForOptions()
	{
		const string pathSegment1 = nameof(pathSegment1),
			pathSegment2 = nameof(pathSegment2),
			pathSegment3 = nameof(pathSegment3),
			headerKey = nameof(headerKey),
			headerValue = nameof(headerValue);

		var expectedOptions = new HttpCallOptions
		{
			PathSegments = { pathSegment1, pathSegment2, pathSegment3 },
			Headers = { { headerKey, headerValue } }
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.WithHeader(headerKey, headerValue)
			.AppendPathSegments(pathSegment1, pathSegment2, pathSegment3)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}

	[Fact]
	public async Task AppendMultipleArraySegments()
	{
		const string pathSegment1 = nameof(pathSegment1),
			pathSegment2 = nameof(pathSegment2),
			pathSegment3 = nameof(pathSegment3),
			pathSegment4 = nameof(pathSegment4);

		var expectedOptions = new HttpCallOptions
		{
			PathSegments = { pathSegment1, pathSegment2, pathSegment3, pathSegment4 }
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.AppendPathSegments(pathSegment1, pathSegment2)
			.AppendPathSegments(pathSegment3, pathSegment4)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}
}
