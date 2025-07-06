using APIBiblioteca.Data;
using APIBiblioteca.Data.Repository;
using APIBiblioteca.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Repositorios
builder.Services.AddScoped<IAutenticacionRepository, AutenticacionRepository>();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("conexionSQL") ?? throw new InvalidOperationException("Conexion no encontrada");
builder.Services.AddDbContext<ApplicationDBContext>(options => 
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
