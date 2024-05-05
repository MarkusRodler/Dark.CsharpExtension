namespace Dark.CsharpExtension;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Benennungsstile", Justification = "Does make more sense in this case")]
public static class BetterTryCatchExtension
{
    public static void Try<T>(Action action, Action<T> catchException) where T : Exception
    {
        try
        {
            action();
        }
        catch (T exception)
        {
            catchException(exception);
        }
    }

    public static Task Catch<E>(this Task task, Action<E> action) where E : Exception
    {
        if (task.Exception?.InnerException is null or not E) return task;

        action.Invoke((E)task.Exception.InnerException);
        return Task.CompletedTask;
    }

    public static Task<T> Catch<E, T>(this Task<T> task, Func<E, Task<T>> action) where E : Exception
        => task.Exception?.InnerException is null or not E ? task : action((E)task.Exception.InnerException);

    public static void Catch<E>(this Action task, Action<E> action) where E : Exception
    {
        try
        {
            task.Invoke();
        }
        catch (E e)
        {
            action.Invoke(e);
        }
    }

    public static T Catch<E, T>(this Func<T> task, Func<E, T> action) where E : Exception
    {
        try
        {
            return task.Invoke();
        }
        catch (E e)
        {
            return action.Invoke(e);
        }
    }
}
