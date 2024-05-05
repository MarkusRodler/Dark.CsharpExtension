using Dark.CsharpExtension;

namespace Test;

[TestClass]
public class BetterTryCatchExtensionTest
{
    [TestMethod]
    public void TryExecutesSpecifiedAction()
    {
        var counter = 0;
        Try(() => counter++, (IndexOutOfRangeException e) => { });

        Assert.AreEqual(counter, 1);
    }

    [TestMethod]
    public void TryCatchesSpecifiedException()
    {
        Exception? catchedException = null;
        Try(() => throw new IndexOutOfRangeException("Err"), (IndexOutOfRangeException e) => catchedException = e);

        Assert.IsTrue(catchedException is IndexOutOfRangeException);
        Assert.AreEqual(catchedException.Message, "Err");
    }

    [TestMethod]
    public void TryDoesNotCatchExceptionIfItIsAnOtherExceptionType() => AssertExt.Throws<Exception>(
        () => Try(() => throw new Exception("Err"), (IndexOutOfRangeException e) => { }),
        "Err"
    );

    [TestMethod]
    public async Task CatchCanCatchExceptionIfTypeMatches()
    {
        var failingTask = Task.FromException<string>(new IndexOutOfRangeException("Index was out of range"));
        Action failingAction = () => throw new IndexOutOfRangeException("Index was out of range");
        Func<string> failingFunction = () => throw new IndexOutOfRangeException("Index was out of range");
        static Task<string> WillFail(string arg)
            => Task.FromException<string>(new IndexOutOfRangeException("Index was out of range"));
        Exception? catchedException1 = null;
        Exception? catchedException2 = null;
        Exception? catchedException3 = null;
        Exception? catchedException4 = null;
        Exception? catchedException5 = null;
        Exception? catchedException6 = null;

        await failingTask.Catch<IndexOutOfRangeException>(e => catchedException1 = e);
        var result1 = await failingTask.Catch<IndexOutOfRangeException, string>(
            (e) => { catchedException2 = e; return Task.FromResult("NOK"); }
        );
        failingAction.Catch<IndexOutOfRangeException>(e => catchedException3 = e);
        var result2 = failingFunction.Catch<IndexOutOfRangeException, object>(
            e => { catchedException4 = e; return "NOK"; }
        );
        await WillFail("Hello?").Catch((IndexOutOfRangeException e) => catchedException5 = e);
        var result3 = await WillFail("Hello?").Catch(
            (IndexOutOfRangeException e) => { catchedException6 = e; return Task.FromResult("NOK"); }
        );

        Assert.IsInstanceOfType<IndexOutOfRangeException>(catchedException1);
        Assert.AreEqual("Index was out of range", catchedException1.Message);
        Assert.IsInstanceOfType<IndexOutOfRangeException>(catchedException2);
        Assert.AreEqual("Index was out of range", catchedException2.Message);
        Assert.IsInstanceOfType<IndexOutOfRangeException>(catchedException3);
        Assert.AreEqual("Index was out of range", catchedException3.Message);
        Assert.IsInstanceOfType<IndexOutOfRangeException>(catchedException4);
        Assert.AreEqual("Index was out of range", catchedException4.Message);
        Assert.IsInstanceOfType<IndexOutOfRangeException>(catchedException5);
        Assert.AreEqual("Index was out of range", catchedException5.Message);
        Assert.IsInstanceOfType<IndexOutOfRangeException>(catchedException6);
        Assert.AreEqual("Index was out of range", catchedException6.Message);
        Assert.AreEqual("NOK", result1);
        Assert.AreEqual("NOK", result2);
        Assert.AreEqual("NOK", result3);
    }

    [TestMethod]
    public async Task CatchWillNotExecuteActionIfNoExceptionIsThrown()
    {
        var task = Task.FromResult("Success");
        Action nonFailingAction = () => { return; };
        Func<string> nonFailingFunc = () => "Success";
        static Task<string> WillNotFail(string arg) => Task.FromResult("Hello " + arg);
        Exception? catchedException1 = null;
        Exception? catchedException2 = null;
        Exception? catchedException3 = null;
        Exception? catchedException4 = null;
        Exception? catchedException5 = null;
        Exception? catchedException6 = null;

        await task.Catch<IndexOutOfRangeException>(e => catchedException1 = e);
        var result1 = await task.Catch<IndexOutOfRangeException, string>(
            e => { catchedException2 = e; return Task.FromResult("NOK"); }
        );
        nonFailingAction.Catch<IndexOutOfRangeException>(e => catchedException3 = e);
        var result2 = nonFailingFunc.Catch<IndexOutOfRangeException, string>(
            e => { catchedException4 = e; return "NOK"; }
        );
        await WillNotFail("World").Catch((IndexOutOfRangeException e) => catchedException5 = e);
        var result3 = await WillNotFail("World").Catch(
            (IndexOutOfRangeException e) => { catchedException6 = e; return Task.FromResult("NOK"); }
        );

        Assert.IsNull(catchedException1);
        Assert.IsNull(catchedException2);
        Assert.IsNull(catchedException3);
        Assert.IsNull(catchedException4);
        Assert.IsNull(catchedException5);
        Assert.IsNull(catchedException6);
        Assert.AreEqual("Success", result1);
        Assert.AreEqual("Success", result2);
        Assert.AreEqual("Hello World", result3);
    }
}
