using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PaperlessDocumentManagement.BusinessServices.DTOs;
using PaperlessDocumentManagement.BusinessServices.Exceptions;
using PaperlessDocumentManagement.BusinessServices.Interfaces;
using PaperlessDocumentManagement.DataAccessLayer.Entities;
using PaperlessDocumentManagement.DataAccessLayer.Interfaces.Repositories;

namespace PaperlessDocumentManagement.BusinessServices.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDocumentDto> _createValidator;
        private readonly ILogger<DocumentService> _logger;

        public DocumentService(
            IDocumentRepository documentRepository,
            IMapper mapper,
            IValidator<CreateDocumentDto> createValidator,
            ILogger<DocumentService> logger)
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
            _createValidator = createValidator;
            _logger = logger;
        }

        public async Task<DocumentDto> GetDocumentByIdAsync(Guid id)
        {
            var document = await _documentRepository.GetDocumentWithDetailsAsync(id)
                ?? throw new DocumentNotFoundException(id);

            return _mapper.Map<DocumentDto>(document);
        }

        public async Task<IEnumerable<DocumentDto>> GetAllDocumentsAsync()
        {
            var documents = await _documentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DocumentDto>>(documents);
        }

        public async Task<DocumentDto> CreateDocumentAsync(CreateDocumentDto createDto, string userId)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var document = new Document
            {
                Title = createDto.Title,
                FileId = createDto.FileId,
                ContentType = createDto.ContentType,
                FileSize = createDto.FileSize,
                CreatedBy = userId,
                Metadata = createDto.Metadata ?? new Dictionary<string, string>()
            };

            var result = await _documentRepository.AddAsync(document);
            _logger.LogInformation("Document created: {DocumentId} by user {UserId}", result.Id, userId);

            return _mapper.Map<DocumentDto>(result);
        }

        public async Task<bool> DeleteDocumentAsync(Guid id, string userId)
        {
            var document = await _documentRepository.GetByIdAsync(id)
                ?? throw new DocumentNotFoundException(id);

            var result = await _documentRepository.DeleteAsync(id);
            if (result)
            {
                _logger.LogInformation("Document deleted: {DocumentId} by user {UserId}", id, userId);
            }

            return result;
        }

        public async Task<bool> UpdateDocumentStatusAsync(Guid id, string status)
        {
            if (!Enum.TryParse<DocumentStatus>(status, true, out var documentStatus))
            {
                throw new DocumentManagementException($"Invalid status: {status}");
            }

            var result = await _documentRepository.UpdateStatusAsync(id, documentStatus);
            if (result)
            {
                _logger.LogInformation("Document status updated: {DocumentId} to {Status}", id, status);
            }

            return result;
        }

        public async Task<DocumentDto> UpdateDocumentAsync(Guid id, UpdateDocumentDto updateDto, string userId)
        {
            var document = await _documentRepository.GetDocumentWithDetailsAsync(id)
                ?? throw new DocumentNotFoundException(id);

            if (updateDto.Title != null)
                document.Title = updateDto.Title;

            if (updateDto.Metadata != null)
                document.Metadata = updateDto.Metadata;

            document.ModifiedBy = userId;
            document.ModifiedAt = DateTime.UtcNow;

            var result = await _documentRepository.UpdateAsync(document);
            _logger.LogInformation("Document updated: {DocumentId} by user {UserId}", id, userId);

            return _mapper.Map<DocumentDto>(result);
        }
    }
}
