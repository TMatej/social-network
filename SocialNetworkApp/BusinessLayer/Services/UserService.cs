using BusinessLayer.DTOs;
using DataAccessLayer.Entity;
using Infrastructure.EFCore.UnitOfWork;

namespace BusinessLayer.Services
{
    public class UserService
    {
        public readonly EFUnitOfWork unitOfWork;

        public UserService(EFUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Register(RegisterDTO registerDTO)
        {
            User user = new User
            {
                Username = registerDTO.Username,
                PasswordHash = registerDTO.Password,
                PrimaryEmail = registerDTO.Email,
            };

            unitOfWork.UserRepository.Insert(user);
            await unitOfWork.Commit();
        }
    }
}
