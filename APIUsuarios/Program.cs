using APIUsuarios;
using APIUsuariosDataAccess.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NgUsersContext>(options =>
{
    options.UseSqlServer("name=ConnectionStrings:DefaultConnection");
    //,b => b.MigrationsAssembly("DataAccess"));
});
//Habilitar la conexion de cualquier origen
builder.Services.AddCors(options => options.AddPolicy("AllowWebApp",
   // builder => builder.WithOrigins("http://localhost:4200")
   builder => builder.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowWebApp");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/users", async (NgUsersContext context) =>
{    
    return await APIService.MostrarListaUsuarios(context);
});

app.MapPost("/users", async (NgUsersContext context, Users user) =>
{
    return await APIService.NuevoUsuario(context, user);
});

app.Run();

