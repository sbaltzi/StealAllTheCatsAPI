using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StealAllTheCatsAPI.Models;
public class TagEntity
{
    /// <summary>
    /// An auto-incremented unique integer that identifies a tag.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Describes the cat’s temperament, returned from the CaaS API (breeds/temperament).
    /// </summary>
    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    /// <summary>
    /// Timestamp of the creation of the database record.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Collection of cats associated with this tag.
    /// </summary>
    public List<CatEntity> Cats { get; set; } = [];
}

