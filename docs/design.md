# Project Overview

The first step in implementing the Pokedex is to focus on building the backend as an MVP (Minimum Viable Product). While doing so, it's important to acknowledge that certain corners might be cut initially, but we will rectify those issues as we approach the product release.

Throughout the development process, the following features will be treated as critical to demonstrate the functionality of the Pokedex MVP:

Querying existing Pokemon from the data source.
Modifying the stats of Pokemon.
Adding new Pokemon to the data source.
Deleting a Pokemon entry from the source.

# API Design

The Pokemon controller is the only controller in the application responsible for interacting with the database. The following APIs are defined for the MVP:

POST /Pokemon: Allows users to add a new Pokemon entry to the data source by providing a Pokemon data model.
GET /Pokemon: Retrieves all Pokemon (800+) from the data source.
GET /pokemon/{name}: Retrieves a specific Pokemon from the data source based on the provided name, if a match is found.

# Things to Look Forward to in the Future

## Paginating the retrieval of all Pokemon models

Currently, the implementation for retrieving all Pokemon models lacks pagination. To enhance performance and handle large datasets, we should introduce pagination functionality. This will allow users to retrieve Pokemon in smaller chunks. Here's an example of how we can implement pagination in the code:

```csharp 
[HttpGet]
public async Task<ActionResult<IEnumerable<Pokemon>>> GetPokemons(int pageSize, int currentPage)
{
    // Validate the input
    if (pageSize <= 0 || currentPage <= 0)
    {
        return BadRequest("Invalid pageSize or currentPage value.");
    }

    try
    {
        // Calculate the skip and take values for pagination
        int itemsToSkipCount = (currentPage - 1) * pageSize;

        // Query the data source with pagination
        var pokemons = await _context.Pokemon
            .Skip(itemsToSkipCount)
            .Take(pageSize)
            .ToListAsync();

        return Ok(pokemons);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "An error occurred while retrieving pokemons.");
        return StatusCode(500, "An error occurred while retrieving pokemons.");
    }
}
```

## Exposing Pokemon list through different filters
To provide more flexibility and enhance the user experience, we can expose Pokemon data through various filters. This will enable users to retrieve specific subsets of Pokemon based on different criteria. Here are some potential filter options:

Get the top N% fastest, strongest, or most defensive Pokemon.
Filter Pokemon by generations or regions (if such information is available).
Retrieve a list of legendary Pokemon.
By implementing these filters, we can offer users more targeted and specialized queries. For example, we can introduce an API to retrieve the top 5% strongest Pokemon based on their raw attack:

```csharp 
[HttpGet("topstrongest")]
public async Task<ActionResult<IEnumerable<Pokemon>>> GetTopStrongestPokemon()
{
    try
    {
        // Calculate the top 5% threshold
        double thresholdPercentage = 0.05;
        int totalCount = await _context.Pokemon.CountAsync();
        int topCount = (int)Math.Ceiling(totalCount * thresholdPercentage);

        // Query the data source to get the top strongest Pokemon
        var topStrongestPokemon = await _context.Pokemon
            .OrderByDescending(p => p.attack)
            .Take(topCount)
            .ToListAsync();

        return Ok(topStrongestPokemon);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "An error occurred while retrieving the top strongest Pokemon.");
        return StatusCode(500, "An error occurred while retrieving the top strongest Pokemon.");
    }
}
```

As the project evolves, we can continue expanding the range of filters and options available to users. By providing these additional features, we can enhance the functionality and usability of the Pokedex, making it a more comprehensive tool for Pokemon enthusiasts.

As the project scales, it is important to consider having specific controllers responsible for particular tasks. This approach avoids having a single "god" class that handles too many responsibilities. Although, at the current scale of the project, having everything within one controller is acceptable. However, it's always good to keep scalability in mind.

# Data Layer

To extract data from the provided CSV file and quickly manipulate it as required by the Pokedex, we'll utilize Sqlite and Entity Framework. For the MVP, the configuration will be treated as in-memory. This allows for fast data manipulation in RAM but does not persist if the web API session ends.

While implementing the MVP, we have decided not to set up a Repository due to the nature of the operations involved. However, as we move from the MVP phase to supporting all features mentioned in the readme, we will revisit this decision.

For future iterations, we suggest considering Redis or any distributed caching system. This can be useful for implementing features like a weekly leaderboard showcasing the most popular searched-for Pokemon.

# Testing the Application

It is crucial to test critical components of the Pokedex application. For example, we can write unit tests for file I/O using the System.IO.Abstractions NuGet package, allowing us to replicate leveraging the file system to import the CSV data to Sqlite at runtime.

Another essential component to test is the PokemonController, as it is a core part of the MVP. We can create tests to verify the expected behavior when valid input is provided. Here's an example:

```csharp 
[Fact]
public async Task GetPokemons_ReturnsOkResultWithListOfPokemons()
{
    // Arrange
    var expectedPokemons = new List<Pokemon>
    {
        new Pokemon { Id = 1, Name = "Bulbasaur" },
        new Pokemon { Id = 2, Name = "Charmander" }
    };

    _mockDbContext.Setup(db => db.Pokemons.ToListAsync()).ReturnsAsync(expectedPokemons);

    // Act
    var result = await _pokemonController.GetPokemons();

    // Assert
    var okResult = Assert.IsType<OkObjectResult>(result.Result);
    var pokemons = Assert.IsAssignableFrom<IEnumerable<Pokemon>>(okResult.Value);
    Assert.Equal(expectedPokemons, pokemons);
}
```