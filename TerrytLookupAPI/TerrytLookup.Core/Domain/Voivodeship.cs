using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TerrytLookup.Core.Domain;

[Index(nameof(Name), IsUnique = true)]
public class Voivodeship : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    public required string Name { get; set; }

    public required ICollection<County> Counties { get; set; }
}