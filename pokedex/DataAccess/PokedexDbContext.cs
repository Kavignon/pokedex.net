using Microsoft.EntityFrameworkCore;

namespace pokedex.DataAccess;

public sealed class PokedexDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<Pokemon> Pokemons { get; set; }
    
    public PokedexDbContext(DbContextOptions<PokedexDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // The key of the pokemon would be its number if it weren't for charizard mega evolutions which sits on the same number...
        // So we'll use the name as the key instead.
        // There are potentials concerns on this approach, such as performance and data duplication, but for the sake of this project, it's fine.
        modelBuilder.Entity<Pokemon>().ToTable("Pokemon").HasKey(p => p.Name);
    }
}

