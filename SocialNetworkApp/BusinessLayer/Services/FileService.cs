using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using System.Configuration;
using File = DataAccessLayer.Entity.File;

namespace BusinessLayer.Services
{
    public class FileService
    {
        public static string filesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["filesPath"] ?? "temp/files");

        public readonly IRepository<File> fileRepo;

        public FileService(IRepository<File> fileRepo)
        {
            this.fileRepo = fileRepo;
        }

        public File saveFile(IFormFile file)
        {
            long size = file.Length;

            if (size == 0)
            {
                throw new ArgumentException("file is empty");
            }

            var fileEntity = new File()
            {
                Name = file.FileName,
                Guid = Guid.NewGuid(),
            };

            fileRepo.Insert(fileEntity);

            using (var stream = System.IO.File.Create($"{filesPath}/{fileEntity.Guid}"))
            {
                file.CopyTo(stream);
            }

            return fileEntity;
        }

        public byte[] GetFile(int id)
        {
            var file = fileRepo.GetByID(id);
            return System.IO.File.ReadAllBytes($"{filesPath}/{file.Guid}");
        }
    }
}
