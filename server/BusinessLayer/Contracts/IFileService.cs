using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Contracts
{
    public interface IFileService : IGenericService<FileEntity>
    {
        public FileEntity CreateFile(IFormFile file);
        public FileEntity GetByGuid(Guid guid);
    }
}
