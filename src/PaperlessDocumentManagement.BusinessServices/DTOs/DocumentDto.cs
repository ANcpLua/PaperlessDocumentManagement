namespace PaperlessDocumentManagement.BusinessServices.DTOs
{
    public class DocumentDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string FileId { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public long FileSize { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public Dictionary<string, string> Metadata { get; set; } = new();
        public List<string> Tags { get; set; } = new();
    }

    public class CreateDocumentDto
    {
        public string Title { get; set; } = null!;
        public string FileId { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public long FileSize { get; set; }
        public Dictionary<string, string>? Metadata { get; set; }
        public List<Guid>? TagIds { get; set; }
    }

    public class UpdateDocumentDto
    {
        public string? Title { get; set; }
        public Dictionary<string, string>? Metadata { get; set; }
        public List<Guid>? TagIds { get; set; }
    }
}
