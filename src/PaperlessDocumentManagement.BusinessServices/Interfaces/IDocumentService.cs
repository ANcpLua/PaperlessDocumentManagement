using PaperlessDocumentManagement.BusinessServices.DTOs;

namespace PaperlessDocumentManagement.BusinessServices.Interfaces
{
    public interface IDocumentService
    {
        Task<DocumentDto> GetDocumentByIdAsync(Guid id);
        Task<IEnumerable<DocumentDto>> GetAllDocumentsAsync();
        Task<DocumentDto> CreateDocumentAsync(CreateDocumentDto createDto, string userId);
        Task<DocumentDto> UpdateDocumentAsync(Guid id, UpdateDocumentDto updateDto, string userId);
        Task<bool> DeleteDocumentAsync(Guid id, string userId);
        Task<bool> UpdateDocumentStatusAsync(Guid id, string status);
    }
}
