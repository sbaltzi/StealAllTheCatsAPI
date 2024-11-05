using Microsoft.EntityFrameworkCore;
using StealAllTheCatsAPI.Models;

namespace StealAllTheCatsAPI.Repositories;
public class CatRepository : ICatRepository
{
    private readonly StealAlltheCatsDbContext stealAlltheCatsDbContext;

    public CatRepository(StealAlltheCatsDbContext stealAlltheCatsDbContext) 
    {
        this.stealAlltheCatsDbContext = stealAlltheCatsDbContext;
    }

    public async Task<CatEntity?> GetCat(int id)
    {
        return await stealAlltheCatsDbContext.Cats.FindAsync(id);
    }
     
    public async Task<IEnumerable<CatEntity>> GetCats(int offset, int numCats)
    {
        return await stealAlltheCatsDbContext.Cats
            .Skip(offset)
            .Take(numCats)
            .ToListAsync();
    }

    public async Task<IEnumerable<CatEntity>> GetCatsByTag(string tagName, int offset, int numCats)
    {
        return await stealAlltheCatsDbContext.Cats
            .Where(cat => cat.Tags.Any(t => t.Name == tagName))
            .Skip(offset)
            .Take(numCats)
            .ToListAsync();
    }

    public async Task<CatEntity> AddCat(CatEntity cat)
    {
        var result = stealAlltheCatsDbContext.Cats.Add(cat);
        await stealAlltheCatsDbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<CatEntity?> UpdateCat(CatEntity cat)
    {
        int id = cat.Id;
        stealAlltheCatsDbContext.Entry(cat).State = EntityState.Modified;

        try
        {
            await stealAlltheCatsDbContext.SaveChangesAsync();
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
        var catEntity = await stealAlltheCatsDbContext.Cats.FindAsync(id);
        if (catEntity != null)
        {
            stealAlltheCatsDbContext.Cats.Remove(catEntity);
            await stealAlltheCatsDbContext.SaveChangesAsync();
        }

        return catEntity;
    }

    private bool CatEntityExists(int id)
    {
        return stealAlltheCatsDbContext.Cats.Any(e => e.Id == id);
    }
}
