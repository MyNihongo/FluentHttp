﻿namespace MyNihongo.FluentHttp.Tests.Unit.FluentHttpTests;

public sealed class WithBasicAuthShould : FluentHttpTestsBase
{
	[Fact]
	public async Task EncodeCredentials()
	{
		const string username = nameof(username),
			password = nameof(password);

		var expectedOptions = new HttpCallOptions
		{
			Headers = { { "Authorization", "QmFzaWMgdXNlcm5hbWU6cGFzc3dvcmQ=" } }
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.WithBasicAuth(username, password)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}
}