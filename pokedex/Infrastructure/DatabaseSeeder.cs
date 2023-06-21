using System.Globalization;
using CsvHelper;
using pokedex.DataAccess;

namespace pokedex.Infrastructure;

internal static class DatabaseSeeder
{
    private static readonly string projectRootDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
    private static readonly string dataAccessDirectoryPath = Path.Combine(projectRootDirectory, "pokedex", "DataAccess");
    private static readonly string pokedexSeedFilePath = Path.Combine(dataAccessDirectoryPath, "pokedex_seed.csv");
    
    internal static void ImportDataFromSeedToDatabase(PokedexDbContext pokedexDbContext)
    {
        pokedexDbContext.Database.EnsureCreated();
        
        using var reader = new StreamReader(pokedexSeedFilePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        
        var pokedexRecords = csv.GetRecords<Pokemon>();
        pokedexDbContext.Pokemons.AddRange(pokedexRecords);
        pokedexDbContext.SaveChanges();
    }
}