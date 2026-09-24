using Microsoft.EntityFrameworkCore;
using MusicDistribution.Application.Interfaces;
using MusicDistribution.Application.Services;
using MusicDistribution.Infrastructure.Data;
using MusicDistribution.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<ITrackRepository, TrackRepository>();
builder.Services.AddScoped<ITrackService, TrackService>();
builder.Services.AddScoped<IDspRepository, DspRepository>();
builder.Services.AddScoped<IDspService, DspService>();
builder.Services.AddScoped<ITrackDistributionRepository, TrackDistributionRepository>();
builder.Services.AddScoped<ITrackDistributionService, TrackDistributionService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler(errorApp => errorApp.Run(async context =>
{
    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    context.Response.ContentType = "application/json";
    await context.Response.WriteAsJsonAsync(new { message = "An unexpected error occurred." });
}));

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
