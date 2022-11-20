using BusinessLayer.DTOs.Photo;
using BusinessLayer.DTOs.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.Gallery
{
    public class GalleryWithProfileRepresentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public ProfileBasicRepresentDTO Profile { get; set; }
    }
}
