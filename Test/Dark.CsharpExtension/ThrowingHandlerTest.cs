namespace Test;

[TestClass]
public class ThrowingHandlerTest
{
    [TestMethod]
    public void IfExecutesActionIfConditionIsMet()
    {
        var counter = 0;

        If(counter == 0, () => counter++);

        Assert.AreEqual(counter, 1);
    }

    [TestMethod]
    public void SendAsyncAlwaysThrowsSpecifiedException()
    {
        ThrowingHandler throwingHandler = new(new ArgumentNullException("Argument null?"));
        using HttpMessageInvoker invoker = new(throwingHandler);

        var ex = AssertExt.Throws<ArgumentNullException>(
            () => invoker.SendAsync(new HttpRequestMessage(), CancellationToken.None),
            "Argument null?"
        );
    }
}
