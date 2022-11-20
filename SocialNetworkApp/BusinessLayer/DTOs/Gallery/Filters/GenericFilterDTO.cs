using System.Linq.Expressions;

namespace BusinessLayer.DTOs.Gallery.Filters
{
    public class GenericFilterDTO /* maybe record */
    {
        public string? WhereColumnName;
        public Expression<Func<int, bool>> FilterWhereExpression;  /* individual class */
        public int? RequestedPageNumber { get; set; }   // For IQuery.Page()
        public int? RequestedPageSize { get; set; }     // For IQuery.Page()
        public string? OrderingColumnName { get; set; }  // For IQuery.OrderBy<T>()
        public bool IsAscending { get; set; } = true;         // For IQuery.OrderBy<T>()
        public IEnumerable<string> IncludeParameters { get; set; }    // For IQuery.Include()
    }
}
