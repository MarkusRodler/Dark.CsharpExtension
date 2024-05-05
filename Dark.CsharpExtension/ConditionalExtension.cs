namespace Dark.CsharpExtension;

public static class ConditionalExtension
{
    public static void If(bool condition, Action then)
    {
        if (condition)
        {
            then();
        }
    }

    public static void Ensure(bool condition, Exception exception)
    {
        if (condition)
        {
            throw exception;
        }
    }
}
