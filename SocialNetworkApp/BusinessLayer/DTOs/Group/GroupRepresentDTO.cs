using BusinessLayer.DTOs.Search;

namespace BusinessLayer.DTOs.Group
{
    public class GroupRepresentDTO : SearchableDTO
    {
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
