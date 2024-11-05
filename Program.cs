using Microsoft.EntityFrameworkCore;
using StealAllTheCatsAPI.Models;
using StealAllTheCatsAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICatRepository, CatRepository>();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
var connectionString = builder.Configuration.GetConnectionString("StealAllTheCatsConnection");
builder.Services.AddDbContext<StealAlltheCatsDbContext>(opt =>
    opt.UseSqlServer(connectionString));
builder.Services.AddRouting(options => options.LowercaseUrls = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(options => options.DescribeAllParametersInCamelCase());

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
