using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.Query.Filters
{
    public class GenericWhereDTO<T>
        where T : IComparable<T>
    {
        public string? WhereColumnName;
        public Expression<Func<T, bool>> FilterWhereExpression;
    }
}
