namespace Dark.CsharpExtension;

public class ThrowingHandler(Exception exception) : DelegatingHandler
{
    readonly Exception exception = exception;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken _)
        => throw exception;
}
