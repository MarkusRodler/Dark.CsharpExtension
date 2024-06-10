namespace Dark.CsharpExtension.Tests
{
    [TestClass]
    public class SucceedingHandlerTests
    {
        [TestMethod]
        public async Task SendAsyncAlwaysReturnsOkWithSpecifiedContent()
        {
            StringContent content = new("Response content");
            using HttpMessageInvoker invoker = new(new SucceedingHandler(content));

            var response = await invoker.SendAsync(new HttpRequestMessage(), CancellationToken.None);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreSame(content, response.Content);
        }
    }
}
