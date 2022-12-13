using BusinessLayer.DTOs.Profile;

namespace BusinessLayer.Facades.Interfaces
{
    public interface IProfileFacade
    {
        ProfileBasicRepresentDTO GetProfileByUserId(int userId);
    }
}
