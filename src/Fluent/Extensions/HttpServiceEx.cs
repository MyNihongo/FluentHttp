namespace MyNihongo.HttpService;

public static class HttpServiceEx
{
	public static IHttpServiceWithOptions AppendPathSegment(this IHttpService @this, string pathSegment) =>
		new HttpServiceWithOptions(@this)
		{
			Options = new HttpCallOptions
			{
				PathSegments = { pathSegment }
			}
		};

	public static IHttpServiceWithOptions AppendPathSegments(this IHttpService @this, params string[] pathSegments)
	{
		var result = new HttpServiceWithOptions(@this);
		result.Options.PathSegments.AddRange(pathSegments);

		return result;
	}
}