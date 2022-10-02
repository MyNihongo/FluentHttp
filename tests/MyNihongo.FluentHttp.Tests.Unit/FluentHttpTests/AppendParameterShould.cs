namespace MyNihongo.FluentHttp.Tests.Unit.FluentHttpTests;

public sealed class AppendParameterShould : FluentHttpTestsBase
{
	[Fact]
	public async Task AppendSingleParameter()
	{
		const string key = nameof(key), value = nameof(value);

		var expectedOptions = new HttpCallOptions
		{
			Parameters =
			{
				{ key, value }
			}
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.AppendParameter(key, value)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}

	[Fact]
	public async Task AppendMultipleParameters()
	{
		const string key1 = nameof(key1), value1 = nameof(value1),
			key2 = nameof(key2), value2 = nameof(value2);

		var expectedOptions = new HttpCallOptions
		{
			Parameters =
			{
				{ key1, value1 },
				{ key2, value2 }
			}
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.AppendParameter(key1, value1)
			.AppendParameter(key2, value2)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}

	[Fact]
	public async Task AppendNonStringValue()
	{
		const string key = nameof(key);
		const long value = 12345678910L;

		var expectedOptions = new HttpCallOptions
		{
			Parameters =
			{
				{ key, "12345678910" }
			}
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.AppendParameter(key, value)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}

	[Fact]
	public async Task MotAppendNullValue()
	{
		const string key = nameof(key);

		var expectedOptions = new HttpCallOptions();

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.AppendParameter(key, null!)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}

	[Fact]
	public async Task AppendSingleParameterWithSegment()
	{
		const string segment = nameof(segment),
			key = nameof(key), value = nameof(value);

		var expectedOptions = new HttpCallOptions
		{
			PathSegments = { segment },
			Parameters =
			{
				{ key, value }
			}
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.AppendPathSegment(segment)
			.AppendParameter(key, value)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}

	[Fact]
	public async Task AppendMultipleParametersWithSegment()
	{
		const string segment = nameof(segment),
			key1 = nameof(key1), value1 = nameof(value1),
			key2 = nameof(key2), value2 = nameof(value2);

		var expectedOptions = new HttpCallOptions
		{
			PathSegments = { segment },
			Parameters =
			{
				{ key1, value1 },
				{ key2, value2 }
			}
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.AppendPathSegment(segment)
			.AppendParameter(key1, value1)
			.AppendParameter(key2, value2)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}

	[Fact]
	public async Task AppendNonStringValueWithSegment()
	{
		const string segment = nameof(segment), key = nameof(key);
		const long value = 12345678910L;

		var expectedOptions = new HttpCallOptions
		{
			PathSegments = { segment },
			Parameters =
			{
				{ key, "12345678910" }
			}
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.AppendPathSegment(segment)
			.AppendParameter(key, value)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}

	[Fact]
	public async Task MotAppendNullValueWithSegment()
	{
		const string segment = nameof(segment), key = nameof(key);

		var expectedOptions = new HttpCallOptions
		{
			PathSegments = { segment }
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.AppendPathSegment(segment)
			.AppendParameter(key, null!)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}
}
