using DataAccessLayer.Entity.Enum;

namespace BusinessLayer.DTOs.Profile
{
    public class AddressDTO {

        public string? State { get; set; }

        public string? Region { get; set; }

        public string? City { get; set; }

        public string? Street { get; set; }

        public string? HouseNumber { get; set; }

        public string? PostalCode { get; set; }
    }

    public class ProfileUpdateDTO
    {
        public string? Name { get; set; }

        public AddressDTO? Address { get; set; }

        public Sex? Sex { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
