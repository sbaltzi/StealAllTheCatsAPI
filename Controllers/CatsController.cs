using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StealAllTheCatsAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace StealAllTheCatsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CatsController : ControllerBase
{
    private readonly StealAlltheCatsDbContext _context;

    public CatsController(StealAlltheCatsDbContext context)
    {
        _context = context;
    }

    // GET: api/cats/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CatEntity>> GetCatEntity(int id)
    {
        var catEntity = await _context.Cats.FindAsync(id);

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

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (string.IsNullOrEmpty(tag))
        {
            var cats = await _context.Cats
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();

            return cats;
        }

        var catsByTag = await _context.Cats
            .Where(cat => cat.Tags.Any(t => t.Name == tag)) // Assuming TagEntity has a Name property
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        return catsByTag;
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

        _context.Entry(catEntity).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CatEntityExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/cats
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<CatEntity>> PostCatEntity(CatEntity catEntity)
    {
        _context.Cats.Add(catEntity);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCatEntity", new { id = catEntity.Id }, catEntity);
    }

    // POST: api/cats/fetch
    [HttpPost("fetch")]
    public async Task<IActionResult> FetchCatEntities()
    {
        const int numberOfCatsToFetch = 25;
        //_context.Cats.Add(catEntity);
        await _context.SaveChangesAsync();

        return Ok($"{numberOfCatsToFetch} new cat images added.");
    }

    // DELETE: api/cats/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCatEntity(int id)
    {
        var catEntity = await _context.Cats.FindAsync(id);
        if (catEntity == null)
        {
            return NotFound();
        }

        _context.Cats.Remove(catEntity);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CatEntityExists(int id)
    {
        return _context.Cats.Any(e => e.Id == id);
    }
}
