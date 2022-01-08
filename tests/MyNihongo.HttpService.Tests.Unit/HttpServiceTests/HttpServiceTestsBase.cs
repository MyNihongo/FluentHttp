using Moq;

namespace MyNihongo.HttpService.Tests.Unit.HttpServiceTests;

public abstract class HttpServiceTestsBase
{
	protected readonly Mock<IHttpService> MockHttpService = new();

	protected IHttpService CreateFixture() =>
		MockHttpService.Object;
}