using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.Query.Filters
{
    public class GenericOrderByDTO<T>
        where T : IComparable<T>
    {
        public string? OrderingColumnName { get; set; }  // For IQuery.OrderBy<T>()
        public bool IsAscending { get; set; } = true;    // For IQuery.OrderBy<T>()
    }
}
