namespace InventoryManagement.Application;

public class Result<T>
{
    private Result(bool isSuccess, T value, string error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public bool IsSuccess { get; }
    public T Value { get; }
    public string Error { get; }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, string.Empty);
    }

    public static Result<T> Failure(string error)
    {
        return new Result<T>(false, default!, error);
    }

    public static Result<T> FailedToFindItem()
    {
        return Failure("Item not found.");
    }

    public static Result<T> FailedToValidate<T1>()
    {
        return Failure($"{nameof(T1)} failed validation.");
    }
}