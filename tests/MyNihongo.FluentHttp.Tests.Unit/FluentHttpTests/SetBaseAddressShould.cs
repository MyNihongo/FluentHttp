namespace MyNihongo.FluentHttp.Tests.Unit.FluentHttpTests;

public sealed class SetBaseAddressShould : FluentHttpTestsBase
{
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	public async Task AppendEmptyBaseAddress(string? baseAddress)
	{
		var expectedOptions = new HttpCallOptions
		{
			BaseAddress = string.Empty
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.SetBaseAddress(baseAddress)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}
	
	[Fact]
	public async Task AppendBaseAddress()
	{
		const string baseAddress = nameof(baseAddress);

		var expectedOptions = new HttpCallOptions
		{
			BaseAddress = baseAddress
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.SetBaseAddress(baseAddress)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}

	[Fact]
	public async Task AppendBaseAddressWithPathSegments()
	{
		const string baseAddress = nameof(baseAddress),
			pathSegment = nameof(pathSegment);

		var expectedOptions = new HttpCallOptions
		{
			BaseAddress = baseAddress,
			PathSegments = { pathSegment }
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.SetBaseAddress(baseAddress)
			.AppendPathSegment(pathSegment)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}

	[Fact]
	public async Task AppendNullUri()
	{
		Uri? baseAddress = null;
		
		var expectedOptions = new HttpCallOptions
		{
			BaseAddress = string.Empty
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.SetBaseAddress(baseAddress)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}

	[Fact]
	public async Task AppendUriAddress()
	{
		const string baseAddressString = "https://github.com/MyNihongo/FluentHttp";
		var baseAddress = new Uri(baseAddressString);
		
		var expectedOptions = new HttpCallOptions
		{
			BaseAddress = baseAddressString
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.SetBaseAddress(baseAddress)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}
}
