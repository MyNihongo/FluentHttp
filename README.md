[![Version](https://img.shields.io/nuget/v/MyNihongo.FluentHttp?style=plastic)](https://www.nuget.org/packages/MyNihongo.FluentHttp/)
[![Nuget downloads](https://img.shields.io/nuget/dt/MyNihongo.FluentHttp?label=nuget%20downloads&logo=nuget&style=plastic)](https://www.nuget.org/packages/MyNihongo.FluentHttp/)   

# FluentHttp
Fluent wrapper around IHttpClientFactory  
Install a NuGet package `MyNihongo.FluentHttp`.

## Configuration
Add a section to `IConfiguration`
```json
{
	"FluentHttp": {
		"BaseAddress": "https://jsonplaceholder.typicode.com",
		"NtlmEnabled": false,
		"Timeout": 100
	} 
}
```
- `Timeout` in seconds. for `Timeout.Infinite` pass -1.

Register a service
```cs
using MyNihongo.FluentHttp;

services.AddFluentHttp();

// Or optionally configure the HTTP client
services.AddFluentHttp((services, httpClient) =>
{
	// configure
});
```

## HTTP methods
To optimize JSON serialization `JsonTypeInfo<T>` can be supplied for all methods. More info about these types [here](https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-source-generator/).  
In further examples a variable `IFluentHttp fluentHttp` will be used.  

#### GetJsonAsync
Gets a JSON stream.
```cs
[JsonSerializable(typeof(RecordContext[]))]
internal partial class RecordContext : JsonSerializerContext {}

public sealed record RecordContext
{
	public int Id { get; set; }
}

// Get the model
var models = await fluentHttp
	.AppendPathSegment("example")
	.GetJsonAsync(RecordContext.Default.RecordArray, ct);
```

#### PostJsonAsync
Posts a JSON model and gets the JSON response.
```cs
[JsonSerializable(typeof(Request))]
internal partial class RequestContext : JsonSerializerContext {}

[JsonSerializable(typeof(Response))]
internal partial class ResponseContext : JsonSerializerContext {}

var req = new Request
{
	Data = "example"
};

var response = await fluentHttp
	.AppendPathSegment("example")
	.PostJsonAsync(req, RequestContext.Default.Request, ResponseContext.Default.Response, ct);
```

## Fluent extensions
Fluent extensions supply additional parameters for the main HTTP methods.

#### AppendPathSegment
Appends a new section to the request URI.
```cs
// get from https://jsonplaceholder.typicode.com/posts
var result = await fluentHttp
	.AppendPathSegment("posts")
	.GetJsonAsync<PostRecord>();
```

#### AppendPathSegments
Appends multiple sections to the request URI.
```cs
// get from https://jsonplaceholder.typicode.com/posts/1/comments
var result = await fluentHttp
	.AppendPathSegment("posts", "1", "comments")
	.GetJsonAsync<PostCommentRecord>();
```

#### WithHeader
Appends a header to the request.
```cs
var result = await fluentHttp
	.AppendPathSegment("posts")
	.WithHeader("my-header", "value")
	.GetJsonAsync<PostRecord>();
```

#### Basic authentication
Append the authentication header for the basic authentication
```cs
var result = await fluentHttp
	.AppendPathSegment("posts")
	.WithBasicAuth("username", "strong password")
	.GetJsonAsync<PostRecord>();
```
