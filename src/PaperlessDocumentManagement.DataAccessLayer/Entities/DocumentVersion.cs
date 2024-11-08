using PaperlessDocumentManagement.DataAccessLayer.Entities.Base;

namespace PaperlessDocumentManagement.DataAccessLayer.Entities
{
    public class DocumentVersion : BaseEntity
    {
        public Guid DocumentId { get; set; }
        public string FileId { get; set; } = null!;
        public int VersionNumber { get; set; }
        public string? ChangeDescription { get; set; }
        public string ContentType { get; set; } = null!;
        public long FileSize { get; set; }
        public virtual Document Document { get; set; } = null!;
    }
}
