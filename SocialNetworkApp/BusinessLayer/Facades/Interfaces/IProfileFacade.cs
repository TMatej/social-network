using BusinessLayer.DTOs.Profile;

namespace BusinessLayer.Facades.Interfaces
{
    public interface IProfileFacade
    {
        ProfileBasicRepresentDTO GetProfileByUserId(int userId);
        void UpdateProfile(int userId, ProfileUpdateDTO profile);
    }
}
