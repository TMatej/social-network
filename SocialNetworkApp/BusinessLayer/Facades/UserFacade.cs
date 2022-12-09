using BusinessLayer.Contracts;
using BusinessLayer.DTOs.User;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades
{
    public class UserFacade : IUserFacade
    {
        private readonly IUserService userService;
        private readonly IUnitOfWork unitOfWork;

        public UserFacade(IUserService userService, IUnitOfWork unitOfWork)
        {
            this.userService = userService;
            this.unitOfWork = unitOfWork;
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
    }
}
