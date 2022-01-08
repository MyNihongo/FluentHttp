using System.Threading.Tasks;
using MyNihongo.HttpService.Tests.Integration.Models;
using Xunit;

namespace MyNihongo.HttpService.Tests.Integration.HttpServiceTests;

public sealed class GetJsonAsyncShould : HttpServiceTestsBase
{
	[Fact]
	public async Task GetData()
	{
		var options = new HttpCallOptions
		{
			PathSegments = new[] { "users" }
		};

		var fixture = await CreateFixture()
			.GetJsonAsync(options, UserRecordContext.Default.UserRecordArray);

		var a = "a";
	}
}