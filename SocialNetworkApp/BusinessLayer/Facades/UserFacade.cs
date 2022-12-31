using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.User;
using BusinessLayer.Facades.Interfaces;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Facades
{
    public class UserFacade : IUserFacade
    {
        private readonly IUserService userService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserFacade(IMapper mapper, IUserService userService, IUnitOfWork unitOfWork)
        {
            this.userService = userService;
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

        public IEnumerable<UserDTO> GetAllUsersPaginated(int page, int size)
        {
            return userService.GetAllUsersPaginated(page, size);
        }

        public void DeleteUser(int userId)
        {
            userService.Delete(userId);
            //no need for "unitOfWork.Commit();" as its implicitly done in GenericService
        }
    }
}
