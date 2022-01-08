using System.Threading;
using Moq;

namespace MyNihongo.HttpService.Tests.Unit.HttpServiceTests;

public abstract class HttpServiceTestsBase
{
	protected readonly Mock<IHttpService> MockHttpService = new();

	protected IHttpService CreateFixture() =>
		MockHttpService.Object;

	internal void VerifyPost(RequestRecord req, HttpCallOptions options, CancellationToken ct)
	{
		MockHttpService
			.Verify(x => x.PostJsonAsync<RequestRecord, ResponseRecord>(req, ItIs.Equivalent(options), null, null, ct), Times.Once);

		MockHttpService.VerifyNoOtherCalls();
	}
}