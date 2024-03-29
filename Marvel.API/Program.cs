using Marvel.API.Filters;
using Marvel.API.Infra;
using Marvel.API.Infra.Repositories;
using Marvel.API.Services;
using Microsoft.EntityFrameworkCore;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MarvelDbContext>(options =>
            options.UseInMemoryDatabase(databaseName: "InMemoryDatabase"));

builder.Services.AddScoped<IMarvelRepository, MarvelRepository>();

builder.Services.AddRefitClient<IMarvelApiService>().ConfigureHttpClient(c =>
{
    var urlApi = builder.Configuration["MarvelApiSettings:ApiUrl"];
    c.BaseAddress = new Uri(urlApi!);
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(CustomExceptionFilter));
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

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

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
