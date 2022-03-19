namespace MyNihongo.FluentHttp.Tests.Unit.FluentHttpTests;

public abstract class FluentHttpTestsBase
{
	protected readonly Mock<IFluentHttp> MockFluentHttp = new();

	protected IFluentHttp CreateFixture() =>
		MockFluentHttp.Object;

	internal void VerifyGet(HttpCallOptions options, CancellationToken ct)
	{
		MockFluentHttp.Verify(x => x.GetJsonAsync<ResponseRecord>(ItIs.Equivalent(options), null, ct), Times.Once);
		VerifyNoOtherCalls();
	}

	internal void VerifyGetOrDefault(HttpCallOptions options, CancellationToken ct)
	{
		MockFluentHttp.Verify(x => x.GetJsonOrDefaultAsync<ResponseRecord>(ItIs.Equivalent(options), null, ct), Times.Once);
		VerifyNoOtherCalls();
	}

	internal void VerifyPost(RequestRecord req, HttpCallOptions options, CancellationToken ct)
	{
		MockFluentHttp.Verify(x => x.PostJsonAsync<RequestRecord, ResponseRecord>(req, ItIs.Equivalent(options), null, null, ct), Times.Once);
		VerifyNoOtherCalls();
	}

	internal void VerifyPostOrDefault(RequestRecord req, HttpCallOptions options, CancellationToken ct)
	{
		MockFluentHttp.Verify(x => x.PostJsonOrDefaultAsync<RequestRecord, ResponseRecord>(req, ItIs.Equivalent(options), null, null, ct), Times.Once);
		VerifyNoOtherCalls();
	}

	private void VerifyNoOtherCalls()
	{
		MockFluentHttp.VerifyNoOtherCalls();
	}
}