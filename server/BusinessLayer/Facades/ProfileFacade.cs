using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Gallery;
using BusinessLayer.DTOs.Profile;
using BusinessLayer.Facades.Interfaces;
using DataAccessLayer.Entity;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades
{
    public class ProfileFacade : IProfileFacade
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProfileService profileService;
        private readonly IGalleryService galleryService;
        private readonly IMapper mapper;

        public ProfileFacade(IMapper mapper, IUnitOfWork unitOfWork, IProfileService profileService, IGalleryService galleryService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.profileService = profileService;
            this.galleryService = galleryService;
        }

        public ProfileBasicRepresentDTO GetProfileByUserId(int userId)
        {
            var profile = profileService.GetByUserId(userId);
            return mapper.Map<ProfileBasicRepresentDTO>(profile);
        }

        public void CreateGallery(int userId, int profileId, GalleryCreateDTO galleryCreateDTO)
        {
            var profile = profileService.GetByUserId(userId);
            if (profile.Id != profileId)
            {
                throw new Exception("User is not owner of profile");
            }
            var gallery = mapper.Map<Gallery>(galleryCreateDTO);
            gallery.ProfileId = profileId;
            galleryService.Insert(gallery);
            unitOfWork.Commit();
        }

        public void DeleteGallery(int userId, int profileId, int galleryId)
        {
            var profile = profileService.GetByID(profileId);
            if (profile.Id != profileId)
            {
                throw new Exception("User is not owner of profile");
            }
            var gallery = galleryService.GetByID(galleryId);
            galleryService.Delete(gallery);
            unitOfWork.Commit();
        }

        public void UpdateProfile(int userId, ProfileUpdateDTO profileUpdateDTO)
        {
          var profile = profileService.GetByUserId(userId);
          profile.Name= profileUpdateDTO.Name;
          profile.Address = mapper.Map<Address>(profileUpdateDTO.Address);
          profile.Sex = profileUpdateDTO.Sex;
          profile.DateOfBirth = profileUpdateDTO.DateOfBirth;
          profile.PhoneNumber = profileUpdateDTO.PhoneNumber;

          profileService.Update(profile);
        }

        public IEnumerable<GalleryRepresentDTO> GetGalleriesByProfileId(int profileId)
        {
            return galleryService.GetGalleriesByProfileId(profileId);
        }
    }
}
