using System.Text.Json.Serialization;

namespace MicroService;

public class Todo
{
    public int Id { get; set; }
    public string Description { get; set; } = "";
}