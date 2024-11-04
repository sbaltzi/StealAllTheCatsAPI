using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace StealAllTheCatsAPI.Models;

public class PaginationParameters
{
    [JsonProperty("page")]
    [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0.")]
    public int Page { get; set; } = 1;

    [JsonProperty("pageSize")]
    [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100.")]
    public int PageSize { get; set; } = 10;
}
