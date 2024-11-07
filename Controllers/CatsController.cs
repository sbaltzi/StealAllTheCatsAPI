using Microsoft.AspNetCore.Mvc;
using StealAllTheCatsAPI.Models;
using StealAllTheCatsAPI.Services;

namespace StealAllTheCatsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CatsController : ControllerBase
{
    private readonly IStealAllTheCatsService _stealAllTheCatsService;

    public CatsController(IStealAllTheCatsService stealAllTheCatsService)
    {
        _stealAllTheCatsService = stealAllTheCatsService;
    }

    // GET: api/cats/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CatEntity>> GetCatEntity(int id)
    {
        var catEntity = await _stealAllTheCatsService.GetCat(id);

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
        var cats = await _stealAllTheCatsService.GetPagedCats(
            pagination.Page, pagination.PageSize, tag);

        return Ok(cats);
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

        var cat = await _stealAllTheCatsService.UpdateCat(catEntity);

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
        var cat = await _stealAllTheCatsService.AddCat(catEntity);
    
        return CreatedAtAction("GetCatEntity", new { id = cat.Id }, cat);
    }

    // POST: api/cats/fetch
    [HttpPost("fetch")]
    public async Task<IActionResult> FetchCatEntities()
    {
        try
        {
            const int numberOfCatsToFetch = 25;
            var cats = await _stealAllTheCatsService.FetchCats(numberOfCatsToFetch);

            // Return the deserialized object (JSON) to the client
            return Ok(cats);
        }
        catch (HttpStatusException exc)
        {
            return StatusCode((int)exc.StatusCode, "Failed to fetch data from TheCatAPI.");
        }
        // return Ok($"{numberOfCatsToFetch} new cat images added.");
    }

    // DELETE: api/cats/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCatEntity(int id)
    {
        var catEntity = await _stealAllTheCatsService.DeleteCat(id);
        if (catEntity == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}
