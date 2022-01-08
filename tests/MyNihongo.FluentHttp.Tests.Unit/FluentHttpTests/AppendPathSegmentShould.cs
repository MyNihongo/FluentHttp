using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyNihongo.FluentHttp.Tests.Unit.FluentHttpTests;

public sealed class AppendPathSegmentShould : FluentHttpTestsBase
{
	[Fact]
	public async Task AppendSingleSegment()
	{
		const string pathSegment = nameof(pathSegment);
		using var cts = new CancellationTokenSource(1);

		var expectedOptions = new HttpCallOptions
		{
			PathSegments = { pathSegment }
		};

		var req = new RequestRecord
		{
			Id = 1
		};

		await CreateFixture()
			.AppendPathSegment(pathSegment)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}

	[Fact]
	public async Task AppendMultipleSegments()
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
			.AppendPathSegment(pathSegment1)
			.AppendPathSegment(pathSegment2)
			.AppendPathSegment(pathSegment3)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}
}