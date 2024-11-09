using System.Xml.Serialization;

namespace TerrytLookup.Infrastructure.Models.Dto.Terryt.Updates;

[XmlRoot("zmiany")]
public class TerrytUpdateDto<T>
{
    [XmlElement("zmiana")]
    public required T[] Changes { get; set; }
}