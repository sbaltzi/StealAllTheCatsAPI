using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StealAllTheCatsAPI.Models;
using StealAllTheCatsAPI.Repositories;

namespace StealAllTheCatsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CatsController : ControllerBase
{
    private readonly string _catsApiKey;
    private readonly ICatRepository _catRepository;
    private readonly HttpClient _httpClient;
    private const string CatApiBaseUrl = "https://api.thecatapi.com/v1/";

    public CatsController(HttpClient httpClient, ICatRepository catRepository, IConfiguration configuration)
    {
        _catsApiKey = configuration["APIKeys:CatsAsAService"];
        _httpClient = httpClient;
        _catRepository = catRepository;
        // Add your API key as a header if required
        _httpClient.DefaultRequestHeaders.Add("x-api-key", _catsApiKey);
    }

    // GET: api/cats/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CatEntity>> GetCatEntity(int id)
    {
        var catEntity = await _catRepository.GetCat(id);

        if (catEntity == null)
        {
            return NotFound();
        }

        return catEntity;
    }

    // GET api/cats?tag=playful&page=1&pageSize=10
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CatEntity>>> GetCats(
        [FromQuery] PaginationParameters pagination,
        [FromQuery] string? tag = null)
    {
        int offset = (pagination.Page - 1) * pagination.PageSize;
        int numCats = pagination.PageSize;

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (string.IsNullOrEmpty(tag))
        {
            var cats = await _catRepository.GetCats(offset, numCats);
            return Ok(cats);
        }

        return Ok(await _catRepository.GetCatsByTag(tag, offset, numCats));
    }

    // PUT: api/cats/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCatEntity(int id, CatEntity catEntity)
    {
        if (id != catEntity.Id)
        {
            return BadRequest();
        }

        var cat = await _catRepository.UpdateCat(catEntity);

        if (cat == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    // POST: api/cats
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<CatEntity>> PostCatEntity(CatEntity catEntity)
    {
        var cat = await _catRepository.AddCat(catEntity);
    
        return CreatedAtAction("GetCatEntity", new { id = cat.Id }, cat);
    }

    // POST: api/cats/fetch
    [HttpPost("fetch")]
    public async Task<IActionResult> FetchCatEntities()
    {
        const int numberOfCatsToFetch = 25;
        var response = await _httpClient.GetAsync($"{CatApiBaseUrl}images/search?limit={numberOfCatsToFetch}");

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode, "Failed to fetch data from TheCatAPI.");
        }

        // Deserialize the JSON response into the model
        var content = await response.Content.ReadAsStringAsync();
        var cats = JsonConvert.DeserializeObject<List<Models.External.CatEntity>>(content);

        //_context.Cats.Add(catEntity);

        // Return the deserialized object (JSON) to the client
        return Ok(cats);

        // return Ok($"{numberOfCatsToFetch} new cat images added.");
    }

    // DELETE: api/cats/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCatEntity(int id)
    {
        var catEntity = await _catRepository.DeleteCat(id);
        if (catEntity == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}
