using StealAllTheCatsAPI.Models;

namespace StealAllTheCatsAPI.Repositories;

public interface ITagRepository
{
    Task<List<TagEntity>> GetOrAddTags(ISet<string> tagNames);
}
