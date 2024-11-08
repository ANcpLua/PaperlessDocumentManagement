using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PaperlessDocumentManagement.DataAccessLayer.Entities;
using PaperlessDocumentManagement.DataAccessLayer.Entities.Context;
using PaperlessDocumentManagement.DataAccessLayer.Infrastructure.Repositories;

namespace PaperlessDocumentManagement.DataAccessLayer.Tests.Repositories
{
    public class DocumentRepositoryTests
    {
        private PaperlessDbContext _context;
        private DocumentRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<PaperlessDbContext>()
                          .UseInMemoryDatabase(databaseName: "PaperlessTest")
                          .Options;

            _context = new PaperlessDbContext(options);
            _repository = new DocumentRepository(_context);
        }

        [Test]
        public async Task AddAsync_ShouldCreateNewDocument()
        {
            // Arrange
            var document = new Document
            {
                Title = "Test Document",
                FileId = "test-file-id",
                ContentType = "application/pdf",
                FileSize = 1024
            };

            // Act
            var result = await _repository.AddAsync(document);

            // Assert
            Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(result.Title, Is.EqualTo("Test Document"));
            Assert.That(result.Status, Is.EqualTo(DocumentStatus.Pending));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
