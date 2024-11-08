using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PaperlessDocumentManagement.BusinessServices.DTOs;
using PaperlessDocumentManagement.BusinessServices.Exceptions;
using PaperlessDocumentManagement.BusinessServices.Mapping;
using PaperlessDocumentManagement.BusinessServices.Services;
using PaperlessDocumentManagement.BusinessServices.Validators;
using PaperlessDocumentManagement.DataAccessLayer.Entities;
using PaperlessDocumentManagement.DataAccessLayer.Interfaces.Repositories;

namespace PaperlessDocumentManagement.BusinessServices.Tests.Services
{
    public class DocumentServiceTests
    {
        private Mock<IDocumentRepository> _documentRepositoryMock;
        private IMapper _mapper;
        private Mock<ILogger<DocumentService>> _loggerMock;
        private IValidator<CreateDocumentDto> _validator;
        private DocumentService _documentService;

        [SetUp]
        public void Setup()
        {
            _documentRepositoryMock = new Mock<IDocumentRepository>();
            
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DocumentMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            
            _loggerMock = new Mock<ILogger<DocumentService>>();
            _validator = new CreateDocumentValidator();
            
            _documentService = new DocumentService(
                _documentRepositoryMock.Object,
                _mapper,
                _validator,
                _loggerMock.Object
            );
        }

        [Test]
        public async Task CreateDocumentAsync_WithValidData_ShouldCreateDocument()
        {
            // Arrange
            var createDto = new CreateDocumentDto
            {
                Title = "Test Document",
                FileId = "test-file-1",
                ContentType = "application/pdf",
                FileSize = 1024,
                Metadata = new Dictionary<string, string>
                {
                    { "department", "HR" }
                }
            };

            var expectedDocument = new Document
            {
                Id = Guid.NewGuid(),
                Title = createDto.Title,
                FileId = createDto.FileId,
                ContentType = createDto.ContentType,
                FileSize = createDto.FileSize,
                Status = DocumentStatus.Pending
            };

            _documentRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Document>()))
                .ReturnsAsync(expectedDocument);

            // Act
            var result = await _documentService.CreateDocumentAsync(createDto, "testUser");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo(createDto.Title));
            Assert.That(result.FileId, Is.EqualTo(createDto.FileId));
            Assert.That(result.Status, Is.EqualTo(DocumentStatus.Pending.ToString()));
        }

        [Test]
        public async Task GetDocumentByIdAsync_WithNonExistingId_ShouldThrowException()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();
            _documentRepositoryMock
                .Setup(x => x.GetDocumentWithDetailsAsync(nonExistingId))
                .ReturnsAsync((Document)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<DocumentNotFoundException>(
                async () => await _documentService.GetDocumentByIdAsync(nonExistingId)
            );
            Assert.That(ex.Message, Does.Contain(nonExistingId.ToString()));
        }

        [Test]
        public async Task UpdateDocumentStatusAsync_WithValidStatus_ShouldUpdateStatus()
        {
            // Arrange
            var documentId = Guid.NewGuid();
            var newStatus = DocumentStatus.OCRComplete.ToString();

            _documentRepositoryMock
                .Setup(x => x.UpdateStatusAsync(documentId, DocumentStatus.OCRComplete))
                .ReturnsAsync(true);

            // Act
            var result = await _documentService.UpdateDocumentStatusAsync(documentId, newStatus);

            // Assert
            Assert.That(result, Is.True);
            _documentRepositoryMock.Verify(
                x => x.UpdateStatusAsync(documentId, DocumentStatus.OCRComplete),
                Times.Once
            );
        }
    }
}
