namespace Microsoft.VisualStudio.TestTools.UnitTesting;

public static class AssertExt
{
    public static T Throws<T>(Action action, string message) where T : Exception
    {
        var exception = Assert.ThrowsException<T>(action);
        Assert.AreEqual(message, exception.Message);
        return exception;
    }

    public static T Throws<T>(Func<object> action, string message) where T : Exception
    {
        var exception = Assert.ThrowsException<T>(action);
        Assert.AreEqual(message, exception.Message);
        return exception;
    }

    public static async Task<T> Throws<T>(Func<Task> action, string message) where T : Exception
    {
        var exception = await Assert.ThrowsExceptionAsync<T>(action);
        Assert.AreEqual(message, exception.Message);
        return exception;
    }

    public static void TimeEquals(DateTime expected, DateTime actual, int timeTolerance) => Assert.IsTrue(
        (actual - expected).TotalSeconds <= timeTolerance,
        $"AssertExt.TimeEquals failed. Expected:<{expected}>. Actual:<{actual}>."
        + $" TimeTolerance:<{timeTolerance}>  Difference:<{(actual - expected).TotalSeconds}>."
    );

    public static void Within<T>(T expectedMinimum, T expectedMaximum, T actual) where T : IComparable<T>
        => Assert.IsTrue(
            actual.CompareTo(expectedMinimum) >= 0 && actual.CompareTo(expectedMaximum) <= 0,
            $"AssertExt.Within failed. Expected between:<{expectedMinimum} - {expectedMaximum}>. Actual:<{actual}>."
        );
}
