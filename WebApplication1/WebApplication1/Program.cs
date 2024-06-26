using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
builder.Services.AddScoped<ITripsRepository, TripsRepository>();

builder.Services.AddDbContext<ApbdContext>( 
    options => options.UseSqlServer("Name=ConnectionStrings:Docker")); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();


app.Run();

