using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyNihongo.HttpService.Tests.Unit.HttpServiceTests;

public sealed class AppendPathSegmentsShould : HttpServiceTestsBase
{
	[Fact]
	public async Task AppendArraySegments()
	{
		const string pathSegment1 = nameof(pathSegment1),
			pathSegment2 = nameof(pathSegment2),
			pathSegment3 = nameof(pathSegment3);
		using var cts = new CancellationTokenSource(1);

		var expectedOptions = new HttpCallOptions
		{
			PathSegments = { pathSegment1, pathSegment2, pathSegment3 }
		};

		var req = new RequestRecord
		{
			Id = 1
		};

		await CreateFixture()
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
		using var cts = new CancellationTokenSource(1);

		var expectedOptions = new HttpCallOptions
		{
			PathSegments = { pathSegment1, pathSegment2, pathSegment3, pathSegment4 }
		};

		var req = new RequestRecord
		{
			Id = 1
		};

		await CreateFixture()
			.AppendPathSegments(pathSegment1, pathSegment2)
			.AppendPathSegments(pathSegment3, pathSegment4)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}
}