using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    [Owned]
    // [Keyless]
    public class Address
    {
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
