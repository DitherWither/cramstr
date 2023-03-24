namespace WebApi.Models;

public class CramstrDatabaseSettings
{
    public string? ConnectionString { get; set; } = null;
    public string? DatabaseName { get; set; } = null;
    public string? FlashCardSetCollectionName { get; set; } = null;
}