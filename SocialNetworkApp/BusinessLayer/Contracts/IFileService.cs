using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Contracts
{
    public interface IFileService
    {

        public FileEntity saveFile(IFormFile file);
        public byte[] GetFile(int id);
    }
}
