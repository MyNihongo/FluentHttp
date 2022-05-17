namespace MyNihongo.FluentHttp;

internal sealed record HttpCallResponse(HttpResponseMessage Response, Stream ResponseStream, string Url);
