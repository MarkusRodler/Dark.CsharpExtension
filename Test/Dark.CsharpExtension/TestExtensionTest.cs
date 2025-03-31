namespace Test;

[TestClass]
public class AssertExtTest
{
    [TestInitialize]
    public void Initialize()
    {
        Thread.CurrentThread.CurrentCulture = new("en-US");
        Thread.CurrentThread.CurrentUICulture = new("en-US");
    }

    [TestMethod]
    public async Task ThrowsChecksIfTheActionThrowsTheSpecifiedException()
    {
        Action action = () => throw new IndexOutOfRangeException("Index was out of range");
        AssertExt.Throws<IndexOutOfRangeException>(action, "Index was out of range");

        Func<object> func = () => throw new IndexOutOfRangeException("Index was out of range");
        AssertExt.Throws<IndexOutOfRangeException>(func, "Index was out of range");

        await AssertExt.Throws<IndexOutOfRangeException>(
            () => throw new IndexOutOfRangeException("Index was out of range"),
            "Index was out of range"
        );
    }

    [TestMethod]
    public async Task ThrowsThrowsAnAssertFailedExceptionIfItDoesNotMatch()
        => await Assert.ThrowsExactlyAsync<AssertFailedException>(
            async () => await AssertExt.Throws<IndexOutOfRangeException>(() => null!, "Index was out of range")
        );

    [TestMethod]
    public void TimeEqualsChecksIfTwoDateTimesAreNearlyIdentical() => AssertExt.TimeEquals(DateTime.Now, DateTime.Now, 1);

    [TestMethod]
    public void TimeEqualsThrowsAssertFailedExceptionIfItIsNotEqual() => AssertExt.Throws<AssertFailedException>(
        () => AssertExt.TimeEquals(new DateTime(2024, 2, 11), new DateTime(2024, 2, 13), 1),
        "Assert.IsTrue failed. AssertExt.TimeEquals failed. Expected:<2/11/2024 12:00:00 AM>. "
        + "Actual:<2/13/2024 12:00:00 AM>. TimeTolerance:<1>  Difference:<172800>."
    );

    [TestMethod]
    public void WithinChecksIfSomethingIsBetweenTwoOtherComparables() => AssertExt.Within(2, 13, 7);

    [TestMethod]
    public void WithinThrowsAssertFailedExceptionIfItIsNotBetween() => AssertExt.Throws<AssertFailedException>(
        () => AssertExt.Within(2, 13, 1),
        "Assert.IsTrue failed. AssertExt.Within failed. Expected between:<2 - 13>. Actual:<1>."
    );
}
