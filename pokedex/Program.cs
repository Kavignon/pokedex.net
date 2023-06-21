using Microsoft.EntityFrameworkCore;
using pokedex.DataAccess;
using pokedex.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PokedexDbContext>(options => options.UseInMemoryDatabase(       
    "PokedexDb"    
));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

LoadDatabaseWithCsvData(builder);

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

static void LoadDatabaseWithCsvData(WebApplicationBuilder builder)
{
    var pokedexDbContext = builder.Services.BuildServiceProvider().GetService<PokedexDbContext>();
    DatabaseSeeder.ImportDataFromSeedToDatabase(pokedexDbContext);
}