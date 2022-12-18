
using BusinessLayer.DTOs.Photo;

namespace BusinessLayer.Facades.Interfaces
{
    public interface IGalleryFacade
    {
        void PostPhoto(int galleryId, PhotoCreateDTO photoCreateDTO);
        void DeletePhoto(int galleryId, int photoId);
    }
}
