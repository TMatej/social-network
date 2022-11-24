using System.Linq.Expressions;

namespace BusinessLayer.DTOs.Query.Filters
{
    public class GenericFilterDTO /* maybe record */
    {
        public int? RequestedPageNumber { get; set; } = null;  // For IQuery.Page()
        public int? RequestedPageSize { get; set; } = null;   // For IQuery.Page()
        public IEnumerable<string> IncludeParameters { get; set; } = new List<string>();   // For IQuery.Include()
    }
}
