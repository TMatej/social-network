using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    [Owned]
    public class Address : IEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(64)]
        public string State { get; set; }

        [MaxLength(64)]
        public string Region { get; set; }

        [MaxLength(64)]
        public string City { get; set; }

        [MaxLength(64)]
        public string Street { get; set; }

        [MaxLength(32)]
        public string HouseNumber { get; set; }

        [MaxLength(32)]
        public string PostalCode { get; set; }
    }
}
