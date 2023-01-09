using BusinessLayer.DTOs.Photo;
using DataAccessLayer.Entity;
using BusinessLayer.DTOs.Gallery;

namespace BusinessLayer.Contracts
{
    public interface IGalleryService : IGenericService<Gallery>
    {
        void UploadPhotoToGallery(PhotoCreateDTO photoDTO, int galleryId);
        GalleryWithPhotosRepresentDTO GetByIdWithPhotos(int id);
        GalleryWithProfileRepresentDTO GetByIdWithProfile(int id);
        GalleryRepresentDTO GetByIdDetailed(int id);
        public IEnumerable<GalleryRepresentDTO> GetGalleriesByProfileId(int profileId);
    }
}
