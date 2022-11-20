using DataAccessLayer.Entity;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using System.Configuration;

namespace BusinessLayer.Services
{
    public class FileService
    {
        public static string filesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["filesPath"] ?? "temp/files");

        public readonly IRepository<FileEntity> fileRepo;

        public FileService(IRepository<FileEntity> fileRepo)
        {
            this.fileRepo = fileRepo;
        }

        public FileEntity saveFile(IFormFile file)
        {
            long size = file.Length;

            if (size == 0)
            {
                throw new ArgumentException("file is empty");
            }

            var fileEntity = new FileEntity()
            {
                Name = file.FileName,
                Guid = Guid.NewGuid(),
            };

            fileRepo.Insert(fileEntity);

            using (var stream = File.Create($"{filesPath}/{fileEntity.Guid}"))
            {
                file.CopyTo(stream);
            }

            return fileEntity;
        }

        public byte[] GetFile(int id)
        {
            var file = fileRepo.GetByID(id);
            return File.ReadAllBytes($"{filesPath}/{file.Guid}");
        }
    }
}
