namespace Test;

[TestClass]
public class ConditionalExtensionTest
{
    [TestMethod]
    public void IfExecutesActionIfConditionIsMet()
    {
        var counter = 0;

        If(counter == 0, () => counter++);

        Assert.AreEqual(counter, 1);
    }

    [TestMethod]
    public void IfDoesNothingIfConditionIsNotMet()
    {
        var counter = 0;

        If(counter == 1, () => counter++);

        Assert.AreEqual(counter, 0);
    }

    [TestMethod]
    public void EnsureThrowsExceptionIfConditionIsMet() => AssertExt.Throws<IndexOutOfRangeException>(
        () => Ensure(true, new IndexOutOfRangeException("Index was out of range")),
        "Index was out of range"
    );

    [TestMethod]
    public void EnsureDoesNothingIfConditionIsNotMet()
    {
        Ensure(false, new IndexOutOfRangeException("Index was out of range"));

        Assert.IsTrue(true);
    }

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
}
