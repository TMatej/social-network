using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.Galery
{
    public class GalleryRepresentDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public int ProfileId { get; set; }

        public Profile Profile { get; set; }

        public string ProfileName { get; set; }

        public string UserName { get; set; }

        public int UserId { get; set; }

        public int PhotosCount { get; set; }
    }
}
