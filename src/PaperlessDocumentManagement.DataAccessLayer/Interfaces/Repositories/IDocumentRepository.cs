using PaperlessDocumentManagement.DataAccessLayer.Entities;

namespace PaperlessDocumentManagement.DataAccessLayer.Interfaces.Repositories
{
    public interface IDocumentRepository : IBaseRepository<Document>
    {
        Task<Document?> GetDocumentWithDetailsAsync(Guid id);
        Task<IEnumerable<Document>> GetDocumentsByStatusAsync(DocumentStatus status);
        Task<bool> UpdateStatusAsync(Guid id, DocumentStatus status);
        Task<bool> AddTagToDocumentAsync(Guid documentId, Guid tagId);
    }
}
