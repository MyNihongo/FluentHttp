using System.Threading;
using Moq;

namespace MyNihongo.FluentHttp.Tests.Unit.FluentHttpTests;

public abstract class FluentHttpTestsBase
{
	protected readonly Mock<IFluentHttp> MockFluentHttp = new();

	protected IFluentHttp CreateFixture() =>
		MockFluentHttp.Object;

	internal void VerifyPost(RequestRecord req, HttpCallOptions options, CancellationToken ct)
	{
		MockFluentHttp
			.Verify(x => x.PostJsonAsync<RequestRecord, ResponseRecord>(req, ItIs.Equivalent(options), null, null, ct), Times.Once);

		MockFluentHttp.VerifyNoOtherCalls();
	}
}