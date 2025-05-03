namespace InventoryManagement.Application;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T Value { get; }
    public string Error { get; }

    private Result(bool isSuccess, T value, string error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value) => new(true, value, string.Empty);
    public static Result<T> Failure(string error) => new(false, default!, error);
    public static Result<T> FailedToFindItem() => Failure("Item not found.");
    public static Result<T> FailedToValidate<T1>() => Failure($"{nameof(T1)} failed validation.");
}
