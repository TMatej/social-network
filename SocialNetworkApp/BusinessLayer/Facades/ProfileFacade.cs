using AutoMapper;
using BusinessLayer.Contracts;
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
        private readonly IMapper mapper;

        public ProfileFacade(IMapper mapper, IUnitOfWork unitOfWork, IProfileService profileService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.profileService = profileService;
        }

        public ProfileBasicRepresentDTO GetProfileByUserId(int userId)
        {
            var profile = profileService.getByUserId(userId);
            return mapper.Map<ProfileBasicRepresentDTO>(profile);
        }

        public void UpdateProfile(int userId, ProfileUpdateDTO profileUpdateDTO)
        {
          var profile = profileService.getByUserId(userId);
          profile.Name= profileUpdateDTO.Name;
          profile.Address = mapper.Map<Address>(profileUpdateDTO.Address);
          profile.Sex = profileUpdateDTO.Sex;
          profile.DateOfBirth = profileUpdateDTO.DateOfBirth;
          profile.PhoneNumber = profileUpdateDTO.PhoneNumber;

          profileService.Update(profile);
        }
    }
}
