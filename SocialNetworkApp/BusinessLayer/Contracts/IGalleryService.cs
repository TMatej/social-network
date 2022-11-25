using BusinessLayer.DTOs.Photo;
using DataAccessLayer.Entity;

namespace BusinessLayer.Contracts
{
    public interface IGalleryService : IGenericService<Gallery>
    {
        void UploadPhotoToGallery(PhotoInsertDTO photoDTO, int galleryId);
        Gallery GetByIdWithListOfPhotos(int id);
    }
}
