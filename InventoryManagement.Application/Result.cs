namespace InventoryManagement.Application;

public sealed record Result<T>(bool IsSuccess, T Value, string Error);

public static class Result
{
    public static Result<T> Success<T>(T value)
    {
        return new Result<T>(true, value, string.Empty);
    }

    private static Result<T> Failure<T>(string error)
    {
        return new Result<T>(false, default!, error);
    }

    public static Result<T> FailedToFindItem<T>()
    {
        return Failure<T>("Item not found.");
    }

    public static Result<T> FailedToValidate<T1, T>()
    {
        return Failure<T>($"{nameof(T1)} failed validation.");
    }
}