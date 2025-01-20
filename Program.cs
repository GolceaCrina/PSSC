using Microsoft.EntityFrameworkCore;
using PSSC_Proiect.Data;
using PSSC_Proiect.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurare servicii
builder.Services.AddControllers();
builder.Services.AddScoped<IComandaRepository, ComandaRepository>();
builder.Services.AddDbContext<ContextAplicatie>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 30))
    )
);

// Adaugare Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurări dezvoltare
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();