using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Group;
using BusinessLayer.DTOs.User;
using BusinessLayer.Facades.Interfaces;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Facades
{
    public class UserFacade : IUserFacade
    {
        private readonly IUserService userService;
        private readonly IGroupService groupService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserFacade(IGroupService groupService, IMapper mapper, IUserService userService, IUnitOfWork unitOfWork)
        {
            this.userService = userService;
            this.groupService = groupService;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public UserDTO GetUserFromCookieAuthId(int id)
        {
            var user = userService.GetByIdDetailed(id);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }
            return mapper.Map<UserDTO>(user);
        }

        public UserDTO Login(UserLoginDTO userLoginDTO)
        {
            var userDto = userService.AuthenticateUser(userLoginDTO);
            if (userDto == null)
            {
                throw new UnauthorizedAccessException();
            }

            return userDto;
        }

        public void Register(UserRegisterDTO userRegisterDTO)
        {
            userService.Register(userRegisterDTO);
            unitOfWork.Commit();
        }

        public void UpdateUserAvatar(int userId, IFormFile avatar)
        {
          userService.ChangeAvatar(userId, avatar);
          unitOfWork.Commit();
        }

        public (long, IEnumerable<UserDTO>) GetAllUsersPaginated(int page, int size)
        {
          
           var result = userService.GetAllUsersPaginated(page, size);
           return (result.TotalItemsCount, mapper.Map<IEnumerable<UserDTO>>(result.Items));
        }

        public void DeleteUser(int userId)
        {
            userService.Delete(userId);
            //no need for "unitOfWork.Commit();" as its implicitly done in GenericService
        }

        public IEnumerable<GroupRepresentDTO> GetGroupsForUser(int userId)
        {
            return groupService.FindGroupsForUser(userId).Select(x => mapper.Map<GroupRepresentDTO>(x));
        }

        public bool CheckPermission(string claimId, int userId)
        {
            var claimInt = int.Parse(claimId);
            return claimInt == userId || userService.IsAdmin(claimInt);
        }
    }
}
