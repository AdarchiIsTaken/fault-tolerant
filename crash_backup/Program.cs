using System.Text.Json;
using Spectre.Console;
using Timer = System.Timers.Timer;

var employee = new Employee{ };
var backupFile = "__backup_employee.json";
if (File.Exists(backupFile)) {
    try {
        employee = JsonSerializer.Deserialize<Employee>(File.OpenRead(backupFile));
        AnsiConsole.MarkupLine("[green]Respaldo cargado[/]");
    } catch (Exception) {
        File.Delete(backupFile);
        AnsiConsole.MarkupLine("[red]No fue posible cargar el respaldo.[/]");
    }
}

if (employee is null) {
    AnsiConsole.MarkupLine("[red]Ocurrio un error inesperado[/]");
    return;
}

var backupTimer = new Timer(2000) {
    Enabled = true
};
backupTimer.Elapsed += (_, _) => {
    File.WriteAllText(backupFile, JsonSerializer.Serialize(employee));
};

var crashTimer = new Timer(10000) {
    Enabled = true
};
crashTimer.Elapsed += (_, _) => {
    AnsiConsole.WriteException(new Exception("Unexpected error"));
    Environment.Exit(0);
};

if (employee.Name == null) {
    employee.Name = AnsiConsole.Ask<string>("Nombre del empleado:");
} else {
    AnsiConsole.MarkupLine($"Nombre: {employee.Name}");
}

if (employee.LastName == null) {
    employee.LastName = AnsiConsole.Ask<string>("Apellidos del empleado:");
} else {
    AnsiConsole.MarkupLine($"Apellidos: {employee.LastName}");
}

if (employee.Birthday == null) {
    employee.Birthday = AnsiConsole.Ask<DateTime>("Fecha de nacimiento del empleado:");
} else {
    AnsiConsole.MarkupLine($"Fecha de nacimiento: {employee.Birthday}");
}

if (employee.Role == null) {
    employee.Role = AnsiConsole.Ask<string>("Rol del empleado:");
} else {
    AnsiConsole.MarkupLine($"Rol: {employee.Role}");
}

public class Employee {
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Role { get; set; }
    
}
