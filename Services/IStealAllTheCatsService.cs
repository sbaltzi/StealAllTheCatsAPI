using StealAllTheCatsAPI.Models;

namespace StealAllTheCatsAPI.Services;

public interface IStealAllTheCatsService
{
    Task<IEnumerable<CatEntity>> FetchCats(int num);
    Task<CatEntity?> GetCat(int id);
    Task<IEnumerable<CatEntity>> GetPagedCats(int page, int pageSize, string? tag);
    Task<CatEntity> AddCat(CatEntity cat);
    Task<CatEntity?> UpdateCat(CatEntity cat);
    Task<CatEntity?> DeleteCat(int id);
}
