namespace PaperlessDocumentManagement.DataAccessLayer.Entities;

public sealed class Tag : BaseEntity
{
    public required string Name { get; set; }
    public required string Color { get; init; }
    public virtual required ICollection<Document> Documents { get; init; } = new HashSet<Document>();
    
    private Tag() : base() { } 

    public static Tag Create(string name, string color) =>
        new() 
        {
            Name = name,
            Color = color
        };
}
