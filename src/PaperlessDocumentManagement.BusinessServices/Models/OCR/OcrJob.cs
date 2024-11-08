namespace PaperlessDocumentManagement.BusinessServices.Models.OCR
{
    public class OcrJob
    {
        public Guid JobId { get; set; } = Guid.NewGuid();
        public Guid DocumentId { get; set; }
        public string FileId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int RetryCount { get; set; }
        public string ContentType { get; set; }
    }

    public class OcrResult
    {
        public Guid JobId { get; set; }
        public Guid DocumentId { get; set; }
        public string ExtractedText { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
    }
}
