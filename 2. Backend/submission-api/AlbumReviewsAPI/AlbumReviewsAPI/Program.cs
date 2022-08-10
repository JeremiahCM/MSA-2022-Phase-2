using AlbumReviewsAPI.Controllers;
using AlbumReviewsAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AlbumReviewsAPIDbContext>(options => options.UseInMemoryDatabase("AlbumReviewsDb"));

builder.Services.AddSingleton<DeezerService>();

builder.Services.AddHttpClient<IDeezerService, DeezerService>(client =>
{
    client.BaseAddress = new Uri("https://api.deezer.com/");
});

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
