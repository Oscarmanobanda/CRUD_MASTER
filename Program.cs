using CRUD.Infraestrcture.Context;
using CRUD.Repositorio.Alumno;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS to allow WinForms / web clients
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// DbContext configuration (Use InMemory DB with sample seed data for easy execution, or MySQL if configured)
var connString = builder.Configuration.GetConnectionString("DefaultConnection");
if (!string.IsNullOrEmpty(connString))
{
    builder.Services.AddDbContext<alumnosContext>(options =>
        options.UseMySql(connString, ServerVersion.AutoDetect(connString)));
}
else
{
    builder.Services.AddDbContext<alumnosContext>(options =>
        options.UseInMemoryDatabase("AlumnosDb"));
}

builder.Services.AddScoped<AlumnoQuery>();
builder.Services.AddScoped<AlumnoCommand>();

var app = builder.Build();

// Seed InMemory database if empty
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<alumnosContext>();
    context.Database.EnsureCreated();
    if (!context.Alumnos.Any())
    {
        context.Alumnos.AddRange(
            new CRUD.Entidades.Alumnos { Idalumnos = 1, Nombres = "Juan", Apellidos = "Pérez", Edad = "22", Fecha = DateTime.Now.ToString("yyyy-MM-dd") },
            new CRUD.Entidades.Alumnos { Idalumnos = 2, Nombres = "Maria", Apellidos = "Gómez", Edad = "20", Fecha = DateTime.Now.ToString("yyyy-MM-dd") },
            new CRUD.Entidades.Alumnos { Idalumnos = 3, Nombres = "Carlos", Apellidos = "López", Edad = "24", Fecha = DateTime.Now.ToString("yyyy-MM-dd") }
        );
        context.SaveChanges();
    }
}

// Configure HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();

