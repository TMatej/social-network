using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.Gallery.Results
{
    public class QueryResultDto<TDto>
    {
        public long TotalItemsCount { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int? RequestedPageSize { get; set; }
        public IEnumerable<TDto> Items { get; set; }
    }
}
