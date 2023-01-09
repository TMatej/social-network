using Microsoft.AspNetCore.Http;

namespace BusinessLayer.DTOs.Photo
{
    public class PhotoCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
    }
}
