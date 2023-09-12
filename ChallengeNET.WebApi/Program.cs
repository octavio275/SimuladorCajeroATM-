using AutoMapper;
using ChallengeNET.Application.Services.Balances;
using ChallengeNET.Application.Services.Operaciones;
using ChallengeNET.Application.Services.Retiros;
using ChallengeNET.Application.Services.Tarjetas;
using ChallengeNET.Application.Services.TarjetaService;
using ChallengeNET.DataAccess;
using ChallengeNET.WebApi;
using EjercicioPOO.Application.CustomExceptionMiddleware;
using EjercicioPOO.Application.Services.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITarjetaService, TarjetaService>();
builder.Services.AddScoped<IBalanceService, BalanceService>();
builder.Services.AddScoped<IRetiroService, RetiroService>();
builder.Services.AddScoped<IOperacionService, OperacionService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Logging.ClearProviders(); // Elimina los proveedores de registro existentes
builder.Logging.AddConsole(); // Agrega el proveedor de registro para la consola
builder.Logging.AddDebug(); // Agrega el proveedor de registro para la salida de depuración


var mapperConfig = new MapperConfiguration(m =>
{
    m.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddDbContext<OperacionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ChallengeConnection"))
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<OperacionContext>();
    dataContext.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
var logger = app.Services.GetRequiredService<ILogger<ExceptionMiddleware>>();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
