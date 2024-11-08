using PaperlessDocumentManagement.BusinessServices.DTOs;

namespace PaperlessDocumentManagement.BusinessServices.Interfaces
{
    public interface ISearchService
    {
        Task IndexDocumentAsync(Guid documentId, string content);
        Task<IEnumerable<DocumentDto>> SearchAsync(string query, int page = 1, int pageSize = 10);
        Task DeleteDocumentFromIndexAsync(Guid documentId);
        Task<bool> DocumentExistsInIndexAsync(Guid documentId);
    }
}
