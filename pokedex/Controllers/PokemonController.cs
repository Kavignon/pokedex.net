using pokedex.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pokedex.Infrastructure;

namespace pokedex.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly PokedexDbContext _context;
        private readonly ILogger<PokemonController> _logger;
        
        public PokemonController(PokedexDbContext context, ILogger<PokemonController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/pokemon
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pokemon>>> GetPokemons()
        {
            var pokemons = await _context.Pokemons.ToListAsync();
            _logger.LogInformation("Displayed all available Pokemon from the DB.");
            return Ok(pokemons);
        }

        // GET: api/pokemon/Charmander
        [HttpGet("{name}")]
        public async Task<ActionResult<Pokemon>> GetPokemon(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger.LogError("The provided pokemon name is null or empty.");

                return BadRequest();
            }

            var pokemon =
                await _context.Pokemons.FirstOrDefaultAsync(
                    p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (pokemon == null)
            {
                _logger.LogError($"The provided pokemon name, {name}, couldn't be matched against records.");

                return NotFound();
            }

            _logger.LogInformation($"The provided pokemon name, {name}, was found and displayed.");
            return Ok(pokemon);
        }

        // POST: api/pokemon
        [HttpPost]
        public async Task<ActionResult<Pokemon>> CreatePokemon(Pokemon pokemon)
        {
            // Perform model validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Pokemons.Add(pokemon);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPokemon), new { name = pokemon.Name }, pokemon);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a Pokémon.");
                return StatusCode(500, "An error occurred while creating the Pokémon.");
            }
        }

        // PUT: api/pokemon/Charmander
        [HttpPut("{name}")]
        public async Task<IActionResult> UpdatePokemon(string name, Pokemon pokemon)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("The provided pokemon model is invalid.");
                return BadRequest(ModelState);
            }
            
            if (!pokemon.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogError($"The provided pokemon name, {name}, doesn't match the pokemon name in the body, {pokemon.Name}.");
                return BadRequest();
            }

            _context.Entry(pokemon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a Pokémon.");
                return StatusCode(500, "An error occurred while updating the Pokémon.");
            }

            _logger.LogInformation($"The provided pokemon name, {name}, was updated.");
            return NoContent();
        }

        // DELETE: api/pokemon/Charmander
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeletePokemon(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger.LogError("The provided pokemon name is null or empty.");

                return BadRequest();
            }
            
            var pokemon =
                await _context.Pokemons.FirstOrDefaultAsync(
                    p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (pokemon == null)
            {
                _logger.LogError($"The provided pokemon name, {name}, couldn't be matched against records.");
                return NotFound();
            }

            _context.Pokemons.Remove(pokemon);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"The provided pokemon name, {name}, was deleted.");
            return NoContent();
        }
    }
}
