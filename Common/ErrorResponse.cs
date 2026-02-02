using System;

namespace ComprasVentas.Common;

public class ErrorResponse
{
    public string TraceId { get; set; }
    
    public int StatusCode { get; set; }

    public string Message { get; set; } = string.Empty;

    public DateTime TimeStamp { get; set; }

    public string Path { get; set; } = string.Empty;
}
