namespace RentalManager.Infrastructure.Options;

public class DocumentServiceOptions
{
    public const string DocumentService = "DocumentService";

    public string Url { get; set; } = string.Empty;

    public string GetDocumentPath { get; set; } = string.Empty;
}