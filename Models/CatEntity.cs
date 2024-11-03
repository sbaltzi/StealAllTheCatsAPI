using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StealAllTheCatsAPI.Models;

public class CatEntity
{
    /// <summary>
    /// An auto-incremented unique integer that identifies a cat.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Represents the ID of the image returned from the CaaS API.
    /// </summary>
    [Required]
    public required string CatId { get; set; }

    /// <summary>
    /// Represents the width of the image returned from the CaaS API.
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Represents the height of the image returned from the CaaS API.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Contains the image data or a solution for storing the image.
    /// </summary>
    [Required]
    public required byte[] Image { get; set; }

    /// <summary>
    /// Timestamp of the creation of the database record.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Collection of tags associated with this cat.
    /// </summary>
    public List<TagEntity> Tags { get; set; } = [];
}

