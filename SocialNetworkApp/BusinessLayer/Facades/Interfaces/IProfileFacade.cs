using BusinessLayer.DTOs.Gallery;
using BusinessLayer.DTOs.Profile;

namespace BusinessLayer.Facades.Interfaces
{
    public interface IProfileFacade
    {
        public ProfileBasicRepresentDTO GetProfileByUserId(int userId);
        public void UpdateProfile(int userId, ProfileUpdateDTO profile);
        public void CreateGallery(int userId, int profileId, GalleryCreateDTO galleryCreateDTO);
        public void DeleteGallery(int userId, int profileId, int galleryId);
        public IEnumerable<GalleryRepresentDTO> GetGalleriesByProfileId(int userId, int profileId);
    }
}
