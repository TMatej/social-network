
using BusinessLayer.DTOs.Profile;

namespace BusinessLayer.Facades
{
    public interface IProfileFacade
    {
      ProfileBasicRepresentDTO GetProfileByUserId(int userId);
    }
}
