using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalManager.Global.Queries;

public class PagedQueryBase
{
    [Required]
    [DefaultValue(true)]
    public bool Descending { get; set; } = true;

    [Required]
    [DefaultValue(10)]
    public int PageSize { get; set; } = 10;

    [Required]
    [DefaultValue(0)]
    public int Position { get; set; } = 0;
}