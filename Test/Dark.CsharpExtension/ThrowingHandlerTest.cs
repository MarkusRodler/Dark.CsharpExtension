namespace Test;

[TestClass]
public class ThrowingHandlerTest
{
    [TestMethod]
    public void IfExecutesActionIfConditionIsMet()
    {
        var counter = 0;

        If(counter == 0, () => counter++);

        Assert.AreEqual(1, counter);
    }

    [TestMethod]
    public async Task SendAsyncAlwaysThrowsSpecifiedException()
    {
        ThrowingHandler throwingHandler = new(new ArgumentNullException("Argument null?"));
        using HttpMessageInvoker invoker = new(throwingHandler);

        await AssertExt.Throws<ArgumentNullException>(
            () => invoker.SendAsync(new HttpRequestMessage(), CancellationToken.None),
            "Value cannot be null. (Parameter 'Argument null?')"
        );
    }
}
