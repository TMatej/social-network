using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.Event
{
    public class EventParticipationDTO
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public int ParticipationTypeId { get; set; }
    }
}
