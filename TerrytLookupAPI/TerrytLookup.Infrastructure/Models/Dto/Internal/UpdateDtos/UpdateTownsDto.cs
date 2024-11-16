namespace TerrytLookup.Infrastructure.Models.Dto.Internal.UpdateDtos;

public class UpdateTownsDto
{
    public UpdateTownUpdateType UpdateType { get; set; }
    
    public int NewVoivodeshipId { get; set; }
}

public enum UpdateTownUpdateType
{
    /// <summary>
    ///     Terryt symbol: <b> D </b>
    /// </summary>
    Add = 0,

    /// <summary>
    ///     Terryt symbol: <b> M </b>
    /// </summary>
    Modify = 1,

    /// <summary>
    ///     Terryt symbol: <b> U </b>
    /// </summary>
    Delete = 2
}