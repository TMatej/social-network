using DataAccessLayer.Entity;
using DataAccessLayer.Entity.Enum;

namespace PresentationLayer.Models
{
    public class ProfileUpsertModel
    {
        public string Name { get; set; }

        public Address Address { get; set; }

        public Sex? Sex { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
