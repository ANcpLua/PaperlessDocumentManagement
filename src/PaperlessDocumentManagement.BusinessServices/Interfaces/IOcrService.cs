using PaperlessDocumentManagement.BusinessServices.Models.OCR;

namespace PaperlessDocumentManagement.BusinessServices.Interfaces
{
    public interface IOcrService
    {
        Task<OcrResult> ProcessDocumentAsync(Stream documentStream, OcrJob job);
        Task<bool> IsFileTypeSupported(string contentType);
    }
}
