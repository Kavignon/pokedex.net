using Microsoft.EntityFrameworkCore;
using pokedex.Models;

namespace pokedex.DataAccess;

internal sealed class PokedexDbContext : DbContext
{
    public DbSet<Pokemon> Pokemons { get; set; }
    
    private readonly IConfiguration configuration;

    public PokedexDbContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = configuration.GetConnectionString("PokedexDbContext");
        optionsBuilder.UseSqlite(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pokemon>().ToTable("Pokemon");
    }
}
