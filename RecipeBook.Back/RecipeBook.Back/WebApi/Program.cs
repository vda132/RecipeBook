using DBLayer.Context;
using Providers.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var allowResources = "AllowResources";

builder.Services.AddCors(options =>
{
    // ToDo: change headers
    options.AddPolicy(name: allowResources,
        builder => builder
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RecipeBookContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnectionVadim"), b => b.MigrationsAssembly("DBLayer")));

builder.Services.AddScoped<IUserProvider, UserProvider>();
builder.Services.AddScoped<IRecipeProvider, RecipeProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowResources);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


app.Run();
