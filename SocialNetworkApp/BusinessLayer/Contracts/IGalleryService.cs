using BusinessLayer.DTOs.Query;
using BusinessLayer.DTOs.Photo;
using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTOs.Gallery;

namespace BusinessLayer.Contracts
{
    public interface IGalleryService : IGenericService<Gallery>
    {
        void UploadPhotoToGallery(PhotoInsertDTO photoDTO, int galleryId);
        GalleryWithPhotosRepresentDTO GetByIdWithPhotos(int id);
        GalleryWithProfileRepresentDTO GetByIdWithProfile(int id);
        GalleryRepresentDTO GetByIdDetailed(int id);
    }
}
