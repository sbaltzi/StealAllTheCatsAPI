using StealAllTheCatsAPI.Models;

namespace StealAllTheCatsAPI.Repositories;

public interface ICatRepository
{
    Task<CatEntity?> GetCat(int id);
    Task<IEnumerable<CatEntity>> GetCats(int offset, int numCats);
    Task<IEnumerable<CatEntity>> GetCatsByTag(string tagName, int offset, int numCats);
    Task<CatEntity> AddCat(CatEntity cat);
    Task<CatEntity?> UpdateCat(CatEntity cat);
    Task<CatEntity?> DeleteCat(int id);
}
