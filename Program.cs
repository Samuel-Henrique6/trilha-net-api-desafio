using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;

var builder = WebApplication.CreateBuilder(args);

// Configura o CORS
builder.Services.AddCors(options =>
{
    // Define uma política chamada "FrontendNext"
    options.AddPolicy(name: "FrontendNext",
                      policy =>
                      {
                          // Permite requisições originadas estritamente de localhost:3000 (HTTP)
                          policy.WithOrigins("http://localhost:3000", "http://127.0.0.1:3000")
                                .AllowAnyHeader() // Permite qualquer header (Content-Type, Authorization, etc.)
                                .AllowAnyMethod(); // Permite métodos GET, POST, PUT, DELETE, etc.
                      });
});

// Add services to the container.
builder.Services.AddDbContext<OrganizadorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();  

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("FrontendNext"); 

app.MapControllers();

app.Run();
