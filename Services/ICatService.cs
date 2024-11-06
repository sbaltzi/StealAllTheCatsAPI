using StealAllTheCatsAPI.Models.External;
using System.Security.Policy;

namespace StealAllTheCatsAPI.Services;

public interface ICatService
{
    Task<IEnumerable<CatEntity>> FetchCats(int numCats);
    Task<byte[]> FetchCatImage(string url);
}
