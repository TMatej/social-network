using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.Event
{
    public class ParticipationTypeDTO
    {
        public virtual int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
