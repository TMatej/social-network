namespace PresentationLayer.Models
{
    public class Paginated<T>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public long Total {get; set;}
        public IEnumerable<T> Items { get; set; }
    }
}
