using BusinessLayer.DTOs.Gallery;
using BusinessLayer.DTOs.Photo;
using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface IGalleryService : IGenericService<Gallery>
    {
        void UploadPhotoToGallery(PhotoInsertDTO photoDTO, int galleryId);
        GalleryRepresentDTO GetByIdWithListOfPhotos(int id);
    }
}
