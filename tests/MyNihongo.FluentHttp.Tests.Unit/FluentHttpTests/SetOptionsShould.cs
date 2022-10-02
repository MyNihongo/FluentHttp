namespace MyNihongo.FluentHttp.Tests.Unit.FluentHttpTests;

public sealed class SetOptionsShould : FluentHttpTestsBase
{
	[Fact]
	public async Task SetCustomOptions()
	{
		const string baseAddress = nameof(baseAddress),
			segment = nameof(segment),
			key = nameof(key), value = nameof(value);

		var expectedOptions = new HttpCallOptions
		{
			BaseAddress = baseAddress,
			PathSegments = { segment },
			Parameters =
			{
				{ key, value }
			}
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.SetOptions(x =>
			{
				x.Parameters.Add(key, value);
				x.PathSegments.Add(segment);
				x.BaseAddress = baseAddress;
			})
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}

	[Fact]
	public async Task AddOptions()
	{
		const string segment1 = nameof(segment1), segment2 = nameof(segment2);

		var expectedOptions = new HttpCallOptions
		{
			PathSegments = { segment1, segment2 }
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.AppendPathSegment(segment1)
			.SetOptions(x =>
			{
				x.PathSegments.Add(segment2);
			})
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}

	[Fact]
	public async Task RemoveOptions()
	{
		const string segment1 = nameof(segment1), segment2 = nameof(segment2);

		var expectedOptions = new HttpCallOptions
		{
			PathSegments = { segment1 }
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.AppendPathSegments(segment1, segment2)
			.SetOptions(x =>
			{
				x.PathSegments.Remove(segment2);
			})
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}
}
