using System.Text.Json.Serialization;

namespace MicroService;

public record TodoResponse {
    public string? Message { get; set; }
    public object? Data { get; set; }
    public bool Status { get; set; } = true;
}