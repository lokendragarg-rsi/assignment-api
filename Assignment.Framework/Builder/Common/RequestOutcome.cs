
namespace Assignment.Framework.Builder.Common;

public class RequestOutcome<T>
{
    public T? Data { get; set; }
    public string? RedirectUrl { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public string? ErrorMessage { get; set; }
    public int StatusCode { get; set; }
}

