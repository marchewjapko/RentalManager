using System.Xml.Serialization;

namespace TerrytLookup.Infrastructure.Models.Dto.Terryt.Updates;

public class SimcUpdateDto
{
    [XmlElement(ElementName = "TypKorekty")]
    public required string UpdateType { get; set; }
    
    [XmlElement(ElementName = "Identyfikator")]
    public required string TerrytId { get; set; }
    
    [XmlElement(ElementName = "WojPrzed")]
    public required string VoivodeshipIdBefore { get; set; }
    
    [XmlElement(ElementName = "GmiPrzed")]
    public required string MunicipalityIdBefore { get; set; }
    
    [XmlElement(ElementName = "RodzPrzed")]
    public required string EntityTypeIdBefore { get; set; }
}