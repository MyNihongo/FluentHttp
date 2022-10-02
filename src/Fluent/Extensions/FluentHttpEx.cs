namespace MyNihongo.FluentHttp;

public static class FluentHttpEx
{
	/// <summary>
	/// Configures options of the HTTP call by using <see cref="options"/> function
	/// </summary>
	/// <param name="this">Instance of the HTTP client provider</param>
	/// <param name="options">Methods to configure options of the HTTP call</param>
	public static IFluentHttpWithOptions SetOptions(this IFluentHttp @this, Action<HttpCallOptions> options)
	{
		var result = new FluentHttpWithOptions(@this);
		options(result.Options);

		return result;
	}

	/// <summary>
	/// Sets <see cref="baseAddress"/> as the base address of the HTTP call
	/// </summary>
	/// <param name="this">Instance of the HTTP client provider</param>
	/// <param name="baseAddress">Base address of the URI</param>
	/// <returns></returns>
	public static IFluentHttpWithOptions SetBaseAddress(this IFluentHttp @this, string baseAddress) =>
		new FluentHttpWithOptions(@this)
		{
			Options = new HttpCallOptions
			{
				BaseAddress = baseAddress
			}
		};

	/// <summary>
	/// Appends <see cref="pathSegment"/> to the base URI of the HTTP call
	/// </summary>
	/// <param name="this">Instance of the HTTP client provider</param>
	/// <param name="pathSegment">Path segment added to the base URI</param>
	public static IFluentHttpWithOptions AppendPathSegment(this IFluentHttp @this, string pathSegment) =>
		new FluentHttpWithOptions(@this)
		{
			Options = new HttpCallOptions
			{
				PathSegments = { pathSegment }
			}
		};

	/// <summary>
	/// Appends <see cref="pathSegments"/> to the base URI of the HTTP call
	/// </summary>
	/// <param name="this">Instance of the HTTP client provider</param>
	/// <param name="pathSegments">Path segments added to the base URI</param>
	public static IFluentHttpWithOptions AppendPathSegments(this IFluentHttp @this, params string[] pathSegments)
	{
		var result = new FluentHttpWithOptions(@this);
		result.Options.PathSegments.AddRange(pathSegments);

		return result;
	}

	/// <summary>
	/// Appends a URL parameter with <see cref="key"/> as the name and <see cref="value"/> as its value
	/// </summary>
	/// <param name="this">Instance of the HTTP client provider</param>
	/// <param name="key">Name of the URL parameter</param>
	/// <param name="value">Value of the URL parameter</param>
	public static IFluentHttpWithOptions AppendParameter(this IFluentHttp @this, string key, object? value)
	{
		var result = new FluentHttpWithOptions(@this);

		var strValue = value?.ToString();
		if (strValue != null)
			result.Options.Parameters.Add(key, strValue);

		return result;
	}

	/// <summary>
	/// Adds the <see cref="header"/> with the <see cref="value"/> to the HTTP call
	/// </summary>
	/// <param name="this">Instance of the HTTP client provider</param>
	/// <param name="header">Key of the header</param>
	/// <param name="value">Value of the header</param>
	public static IFluentHttpWithOptions WithHeader(this IFluentHttp @this, string header, string value) =>
		new FluentHttpWithOptions(@this)
		{
			Options = new HttpCallOptions
			{
				Headers =
				{
					{ header, value }
				}
			}
		};

	/// <summary>
	/// Adds the Basic Authentication header with the <see cref="username"/> and the <see cref="password"/> to the HTTP call
	/// </summary>
	/// <param name="this">Instance of the HTTP client provider</param>
	/// <param name="username">Username of the authenticating user</param>
	/// <param name="password">Password of the authenticating user</param>
	public static IFluentHttpWithOptions WithBasicAuth(this IFluentHttp @this, string username, string password)
	{
		var (header, value) = AuthUtils.BasicAuth(username, password);
		return @this.WithHeader(header, value);
	}
}
