using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    class FileUploadRepository : GenericRepository<FileUpload, string>, IFileUploadRepository
    {
        public FileUploadRepository(TravellinDbContext dbContext) : base(dbContext)
        { }
    }
}
