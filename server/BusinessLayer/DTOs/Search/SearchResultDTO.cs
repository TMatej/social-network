using BusinessLayer.DTOs.FileEntity;

namespace BusinessLayer.DTOs.Search
{
    public class SearchResultDTO
    {
        public string Type { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public FileEntityDTO? Image { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
