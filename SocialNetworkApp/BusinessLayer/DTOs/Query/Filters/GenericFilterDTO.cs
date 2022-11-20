using System.Linq.Expressions;

namespace BusinessLayer.DTOs.Query.Filters
{
    public class GenericFilterDTO /* maybe record */
    {
        public int? RequestedPageNumber { get; set; }   // For IQuery.Page()
        public int? RequestedPageSize { get; set; }     // For IQuery.Page()
        public IEnumerable<string> IncludeParameters { get; set; }    // For IQuery.Include()
    }
}
