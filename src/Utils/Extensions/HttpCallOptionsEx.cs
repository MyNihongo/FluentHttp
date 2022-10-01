namespace MyNihongo.FluentHttp;

internal static class HttpCallOptionsEx
{
	public static Uri CreateUri(this HttpCallOptions @this)
	{
		UriKind uriKind;
		StringBuilder uriStringBuilder;

		if (string.IsNullOrEmpty(@this.BaseAddress))
		{
			uriStringBuilder = @this.PathSegments.Join(Const.UriSeparator);
			uriKind = UriKind.Relative;
		}
		else
		{
			uriStringBuilder = @this.BaseAddress.Join(@this.PathSegments, Const.UriSeparator);
			uriKind = UriKind.Absolute;
		}

		if (@this.Parameters.Count != 0)
		{
			uriStringBuilder
				.Append('?');

			var i = 0;
			foreach (var (key, value) in @this.Parameters)
			{
				if (i != 0)
					uriStringBuilder.Append('&');

				uriStringBuilder.Append(key).Append('=').Append(value);
				i++;
			}
		}

		return new Uri(uriStringBuilder.ToString(), uriKind);
	}
}
