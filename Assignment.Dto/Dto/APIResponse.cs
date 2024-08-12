using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Assignment.Dto.Dto;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class APIResponse<T>
{
    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    /// <value>
    /// The status.
    /// </value>
    public string ApiResponseStatus { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    /// <value>
    /// The status.
    /// </value>
    public int? HttpStatus { set; get; }

    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    /// <value>
    /// The message.
    /// </value>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the content.
    /// </summary>
    /// <value>
    /// The content.
    /// </value>
    public T Data { get; set; }

    /// <summary>
    /// Gets or sets the errors.
    /// </summary>
    /// <value>
    /// The errors.
    /// </value>
    public List<Error.Error> Errors { get; set; }
}
