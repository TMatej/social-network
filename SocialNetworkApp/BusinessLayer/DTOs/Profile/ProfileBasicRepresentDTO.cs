using BusinessLayer.DTOs.User;
using DataAccessLayer.Entity;
using DataAccessLayer.Entity.Enum;

namespace BusinessLayer.DTOs.Profile
{
    public class ProfileBasicRepresentDTO
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public Sex? Sex { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDTO User { get; set; }
    }
}
