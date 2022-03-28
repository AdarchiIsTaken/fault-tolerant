using System.Text.Json.Serialization;
using MicroService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(o => {
        o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<TodoStore>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseDefaultFiles(new DefaultFilesOptions{
    DefaultFileNames = new List<string>{ "index.html" }
});
app.UseStaticFiles();

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
