using Microsoft.EntityFrameworkCore;
using PaperlessDocumentManagement.DataAccessLayer.Entities;
using PaperlessDocumentManagement.DataAccessLayer.Entities.Context;
using PaperlessDocumentManagement.DataAccessLayer.Interfaces.Repositories;

namespace PaperlessDocumentManagement.DataAccessLayer.Infrastructure.Repositories
{
    public class DocumentRepository : BaseRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(PaperlessDbContext context) : base(context)
        {
        }

        public async Task<Document?> GetDocumentWithDetailsAsync(Guid id)
        {
            return await _dbSet
                         .Include(d => d.Tags)
                         .Include(d => d.Versions)
                         .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Document>> GetDocumentsByStatusAsync(DocumentStatus status)
        {
            return await _dbSet
                         .Where(d => d.Status == status)
                         .ToListAsync();
        }

        public async Task<bool> UpdateStatusAsync(Guid id, DocumentStatus status)
        {
            var document = await GetByIdAsync(id);
            if (document == null) return false;

            document.Status = status;
            document.ModifiedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddTagToDocumentAsync(Guid documentId, Guid tagId)
        {
            var document = await GetDocumentWithDetailsAsync(documentId);
            var tag = await _context.Tags.FindAsync(tagId);

            if (document == null || tag == null) return false;

            document.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
