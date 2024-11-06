using Microsoft.EntityFrameworkCore;
using StealAllTheCatsAPI.Models;

namespace StealAllTheCatsAPI.Repositories;
public class CatRepository : ICatRepository
{
    private readonly StealAlltheCatsDbContext _context;

    public CatRepository(StealAlltheCatsDbContext stealAlltheCatsDbContext) 
    {
        this._context = stealAlltheCatsDbContext;
    }

    public async Task<CatEntity?> GetCat(int id)
    {
        return await _context.Cats.FindAsync(id);
    }
     
    public async Task<IEnumerable<CatEntity>> GetCats(int offset, int numCats)
    {
        return await _context.Cats
            .Skip(offset)
            .Take(numCats)
            .ToListAsync();
    }

    public async Task<IEnumerable<CatEntity>> GetCatsByTag(string tagName, int offset, int numCats)
    {
        return await _context.Cats
            .Where(cat => cat.Tags.Any(t => t.Name == tagName))
            .Skip(offset)
            .Take(numCats)
            .ToListAsync();
    }

    public async Task<CatEntity> AddCat(CatEntity cat)
    {
        var result = _context.Cats.Add(cat);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<CatEntity?> UpdateCat(CatEntity cat)
    {
        int id = cat.Id;
        _context.Entry(cat).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CatEntityExists(id))
            {
                return null;
            }
            else
            {
                throw;
            }
        }
        return cat;
    }

    public async Task<CatEntity?> DeleteCat(int id)
    {
        var catEntity = await _context.Cats.FindAsync(id);
        if (catEntity != null)
        {
            _context.Cats.Remove(catEntity);
            await _context.SaveChangesAsync();
        }

        return catEntity;
    }

    private bool CatEntityExists(int id)
    {
        return _context.Cats.Any(e => e.Id == id);
    }
}
