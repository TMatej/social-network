using BusinessLayer.DTOs.Photo;
using DataAccessLayer.Entity;

namespace BusinessLayer.Contracts
{
    public interface IPhotoService : IGenericService<Photo>
    {
        void UploadPhotoToGallery(PhotoInsertDTO photoDTO);
    }
}
