using PaperlessDocumentManagement.DataAccessLayer.Entities.Base;
using System.Collections.Generic;

namespace PaperlessDocumentManagement.DataAccessLayer.Entities
{
    public class Document : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string FileId { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public long FileSize { get; set; }
        public DocumentStatus Status { get; set; }
        public string? OcrText { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<DocumentVersion> Versions { get; set; }

        public Document() : base()
        {
            Status = DocumentStatus.Pending;
            Metadata = new Dictionary<string, string>();
            Tags = new HashSet<Tag>();
            Versions = new HashSet<DocumentVersion>();
        }
    }

    public enum DocumentStatus
    {
        Pending = 0,
        Processing = 1,
        OCRComplete = 2,
        IndexingComplete = 3,
        Error = 4
    }
}
