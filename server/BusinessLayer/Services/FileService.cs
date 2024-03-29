﻿using BusinessLayer.Contracts;
using DataAccessLayer.Entity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Services
{
    public class FileService : GenericService<FileEntity>, IFileService
    {
        public readonly IRepository<FileEntity> fileRepo;
        public IQuery<FileEntity> fileQuery;

        public FileService(IRepository<FileEntity> fileRepo, IQuery<FileEntity> fileQuery, IUnitOfWork uow) : base(fileRepo, uow)
        {
            this.fileRepo = fileRepo;
            this.fileQuery = fileQuery;
        }

        public FileEntity CreateFile(IFormFile file)
        {
            long size = file.Length;

            if (size == 0)
            {
                throw new ArgumentException("file is empty");
            }

            using(var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();

                var fileEntity = new FileEntity
                {
                    Data = fileBytes,
                    Guid = Guid.NewGuid(),
                    Name = file.FileName,
                    FileType = file.ContentType,
                };

                return fileEntity;
            }
        }

        public FileEntity GetByGuid(Guid guid)
        {
          return fileQuery.Where<Guid>(x => x == guid, nameof(FileEntity.Guid)).Execute().Items.FirstOrDefault();
        }
    }
}
