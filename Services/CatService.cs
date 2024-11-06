using Newtonsoft.Json;
using StealAllTheCatsAPI.Models.External;
using System.Net;
using System.Net.Http;

namespace StealAllTheCatsAPI.Services;

public class HttpStatusException(HttpStatusCode statusCode) : Exception
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}

public class CatService : ICatService
{
    private const string CatApiBaseUrl = "https://api.thecatapi.com/v1/";
    private readonly string _catsApiKey;
    private readonly HttpClient _httpClient;

    public CatService(HttpClient httpClient, IConfiguration configuration) {
        _catsApiKey = configuration["APIKeys:CatsAsAService"]
            ?? throw new ArgumentNullException(
                "API key for CatsAsAService is missing in configuration");
        _httpClient = httpClient;
        // Add your API key as a header if required
        _httpClient.DefaultRequestHeaders.Add("x-api-key", _catsApiKey);
    }

    public async Task<byte[]> FetchCatImage(string url)
    {
        return await _httpClient.GetByteArrayAsync(url);
    }

    public async Task<IEnumerable<CatEntity>> FetchCats(int numCats)
    {
        var uri = $"{CatApiBaseUrl}images/search?limit={numCats}";
        var response = await _httpClient.GetAsync(uri);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpStatusException(response.StatusCode);
        }

        // Deserialize the JSON response into the model
        var content = await response.Content.ReadAsStringAsync();
        var cats = JsonConvert.DeserializeObject<List<CatEntity>>(content);

        return cats ?? Enumerable.Empty<CatEntity>();
    }
}
