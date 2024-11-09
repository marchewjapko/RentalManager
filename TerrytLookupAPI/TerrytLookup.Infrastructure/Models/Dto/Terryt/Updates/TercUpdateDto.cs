using System.Xml.Serialization;

namespace TerrytLookup.Infrastructure.Models.Dto.Terryt.Updates;

public class TercUpdateDto
{
    [XmlElement(ElementName = "TypKorekty")]
    public required string UpdateType { get; set; }

    [XmlElement(ElementName = "WojPrzed")]
    public required string VoivodeshipIdBefore { get; set; }

    [XmlElement(ElementName = "PowPrzed")]
    public required string CountyIdBefore { get; set; }

    [XmlElement(ElementName = "GmiPrzed")]
    public required string MunicipalityIdBefore { get; set; }

    [XmlElement(ElementName = "RodzPrzed")]
    public required string EntityTypeIdBefore { get; set; }

    [XmlElement(ElementName = "NazwaPrzed")]
    public required string NameBefore { get; set; }

    [XmlElement(ElementName = "StanPrzed")]
    public DateTime ValidFromBefore { get; set; }

    [XmlElement(ElementName = "WojPo")]
    public required string VoivodeshipIdAfter { get; set; }

    [XmlElement(ElementName = "PowPo")]
    public required string CountyIdAfter { get; set; }

    [XmlElement(ElementName = "GmiPo")]
    public required string MunicipalityIdAfter { get; set; }

    [XmlElement(ElementName = "RodzPo")]
    public required string EntityTypeIdAfter { get; set; }

    [XmlElement(ElementName = "NazwaPo")]
    public required string NameAfter { get; set; }

    [XmlElement(ElementName = "StanPo")]
    public DateTime ValidFromAfter { get; set; }
}