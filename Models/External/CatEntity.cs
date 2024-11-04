namespace StealAllTheCatsAPI.Models.External;
public class CatEntity
{
    public string id { get; set; }
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public List<BreedEntity> breeds { get; set; } = [];
}
