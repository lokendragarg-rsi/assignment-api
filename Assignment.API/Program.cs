using Assignment.Services.MemoryCacheServices;
using Assignment.Services.StoryAPIService;
using Assignment.Services.StoryServices;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(typeof(IMemoryCache), typeof(MemoryCache));
builder.Services.AddTransient(typeof(HttpClient));
builder.Services.AddScoped(typeof(IStoryService), typeof(StoryService));
builder.Services.AddScoped(typeof(IStoryApiService), typeof(StoryApiService));
builder.Services.AddScoped(typeof(IMemoryCacheService), typeof(MemoryCacheService));

builder.Services.AddCors(o => o.AddPolicy("CorePolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorePolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
