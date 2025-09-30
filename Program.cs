using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
}

if (app.Configuration.GetValue<bool>("UseDeveloperExceptionPage"))
    app.UseDeveloperExceptionPage();
else
    app.UseExceptionHandler("/test");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/test", () => Results.Problem());
app.MapGet("/error/test", () => { throw new Exception("test"); });
app.MapControllers();

app.Run();
