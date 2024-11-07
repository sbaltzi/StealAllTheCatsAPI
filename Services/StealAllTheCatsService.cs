using Azure;
using StealAllTheCatsAPI.Models;
using StealAllTheCatsAPI.Repositories;

namespace StealAllTheCatsAPI.Services;

public class StealAllTheCatsService : IStealAllTheCatsService
{
    private readonly ICatRepository _catRepository;
    private readonly ITagRepository _tagRepository;
    private readonly ICatService _catService;

    public StealAllTheCatsService(
        ICatRepository catRepository,
        ITagRepository tagRepository,
        ICatService catService) {
        _catRepository = catRepository;
        _tagRepository = tagRepository;
        _catService = catService;
    }

    public async Task<CatEntity> AddCat(CatEntity cat)
    {
        return await _catRepository.AddCat(cat);
    }

    public async Task<CatEntity?> DeleteCat(int id)
    {
        return await _catRepository.DeleteCat(id);
    }

    public async Task<IEnumerable<CatEntity>> FetchCats(int num)
    {
       var newCats = new List<CatEntity>();

        foreach (var externalCat in await _catService.FetchCats(num))
        {
            // Download JPEG image as a byte array.
            var imageData = await _catService.FetchCatImage(externalCat.url);

            // Store the tags into the database.
            var tags = BreedsToTags(externalCat.breeds);
            var storedTags = await _tagRepository.GetOrAddTags (tags.ToHashSet());

            var catEntity = new CatEntity
            {
                CatId = externalCat.id,
                Width = externalCat.width,
                Height = externalCat.height,
                Image = imageData,
                Tags = storedTags
            };

            var storedCat = await _catRepository.AddCat(catEntity);
            newCats.Add(storedCat);
        }
        return newCats;
    }
    
    private List<string> BreedsToTags(List<Models.External.BreedEntity> breeds)
    {
        return breeds?
            .SelectMany(breed => breed.Temperament.Split(','))
            .Select(temperament => temperament.Trim())
            .ToList()
       ?? [];
    }

    public async Task<CatEntity?> GetCat(int id)
    {
        return await _catRepository.GetCat(id);
    }

    public async Task<IEnumerable<CatEntity>> GetPagedCats(
        int page, int pageSize, string? tag)
    {
        int offset = (page - 1) * pageSize;
        int numCats = pageSize;

        if (string.IsNullOrEmpty(tag))
        {
            return await _catRepository.GetCats(offset, numCats);
        }

        return await _catRepository.GetCatsByTag(tag, offset, numCats);
    }

    public async Task<CatEntity?> UpdateCat(CatEntity cat)
    {
        return await _catRepository.UpdateCat(cat);
    }
}
