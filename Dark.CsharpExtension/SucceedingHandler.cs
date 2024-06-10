namespace Dark.CsharpExtension;

public class SucceedingHandler(StringContent content) : DelegatingHandler
{
    readonly StringContent content = content;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken _)
        => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = content });
}
