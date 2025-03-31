# Dark.CsharpExtension (Library)
![Library](https://github.com/MarkusRodler/Dark.CsharpExtension/workflows/Library/badge.svg)

## How to use

### Try (only for Actions)
```csharp
Try(DangerousAction(), (WtfHappenedException e) => { });
```

### Catch Exception from Task, Action or Func
```csharp
await failingTask.Catch<IndexOutOfRangeException>(e => HandleException(e));
var result1 = await failingTask.Catch<IndexOutOfRangeException, string>(
    (e) => { HandleException(e); return Task.FromResult("NOK"); }
);

failingAction.Catch<IndexOutOfRangeException>(e => HandleException(e););
var result2 = failingFunction.Catch<IndexOutOfRangeException, object>(
    e => { HandleException(e); return "NOK"; }
);

await WillFail("Hello?").Catch((IndexOutOfRangeException e) => HandleException(e););
var result3 = await WillFail("Hello?").Catch(
    (IndexOutOfRangeException e) => { HandleException(e); return Task.FromResult("NOK"); }
);

### If
```csharp
If(condition, () => Console.WriteLine("Yay"));
```

### Ensure
```csharp
Ensure(condition, new IndexOutOfRangeException("Index was out of range")),
```

### AssertExt
```csharp
[TestClass]
public class AssertExtTest
{
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
        => await Assert.ThrowsExceptionAsync<AssertFailedException>(
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
```

### DelegatingHandlers
https://github.com/rmandvikar/delegating-handlers/tree/main also has a bunch of additional handlers

#### SuccessingHandler (useful to test DelegatingHandler)
```csharp
    SucceedingHandler succeedingHandler = new(new StringContent("Response content"))
    using HttpMessageInvoker invoker = new(succeedingHandler);
```

#### ThrowingHandler (useful to test DelegatingHandler)
```csharp
    ThrowingHandler throwingHandler = new(new ArgumentNullException("Argument null?"));
    using HttpMessageInvoker invoker = new(throwingHandler);
```
