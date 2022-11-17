using AutoMapper;
using BusinessLayer.Config;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Galery;
using DataAccessLayer.Entity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class GalleryService : GenericService<Gallery>, IGalleryService
    {
        public GalleryService(IRepository<Gallery> repository, IMapper mapper, IUnitOfWork uow) : base(repository, mapper, uow)
        {
        }


    }
}
