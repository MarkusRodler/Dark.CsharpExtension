namespace Test;

[TestClass]
public class ConditionalExtensionTest
{
    [TestMethod]
    public void IfExecutesActionIfConditionIsMet()
    {
        var counter = 0;

        If(counter == 0, () => counter++);

        Assert.AreEqual(1, counter);
    }

    [TestMethod]
    public void IfDoesNothingIfConditionIsNotMet()
    {
        var counter = 0;

        If(counter == 1, () => counter++);

        Assert.AreEqual(0, counter);
    }

    [TestMethod]
    public void EnsureThrowsExceptionIfConditionIsMet() => AssertExt.Throws<IndexOutOfRangeException>(
        () => Ensure(true, new IndexOutOfRangeException("Index was out of range")),
        "Index was out of range"
    );

    [TestMethod]
    public void EnsureDoesNothingIfConditionIsNotMet()
        => Ensure(false, new IndexOutOfRangeException("Index was out of range"));
}
