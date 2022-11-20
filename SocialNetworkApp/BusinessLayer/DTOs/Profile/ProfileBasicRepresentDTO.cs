using DataAccessLayer.Entity.Enum;
using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int UserId { get; set; }
    }
}
