namespace Utilities.Opeartions;
public class OperationResult
{
    public OperationResult(bool success, string? message)
    {
        Success = success;
        Message = message;
    }

    public bool Success { get; private set; }
    public string? Message { get; private set; }
    public static OperationResult Succeded() => new OperationResult(true, null);
    public static OperationResult Faild(string message) => new OperationResult(false, message);
}
