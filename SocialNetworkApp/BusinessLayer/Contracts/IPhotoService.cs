using BusinessLayer.DTOs.Photo;
using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface IPhotoService : IGenericService<Photo>
    {
        void UploadPhotoToGallery(PhotoInsertDTO photoDTO);
    }
}
