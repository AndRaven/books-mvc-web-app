
using static Utilities;

public class RequestDto
{
    public RequestType RequestType { get; set; } = RequestType.GET;

    public string Url { get; set; } = string.Empty;

    public object? Data { get; set; }
}