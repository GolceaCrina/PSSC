using Microsoft.EntityFrameworkCore;
using PSSC_Proiect.Data;
using PSSC_Proiect.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IComandaRepository, ComandaRepository>();

// Adăugăm configurația bazei de date
builder.Services.AddDbContext<ContextAplicatie>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 30))
    )
);

// Adăugăm suport pentru Swagger (opțional, util pentru testare)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Activăm Swagger în modul Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();