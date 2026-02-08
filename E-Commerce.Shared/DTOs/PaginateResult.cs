using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOs
{
    public class PaginateResult<TDTO>
    {
        public int Page { get; set; }
        public int PageIndex => Page;
        public int PageSize { get; set; }
        public int ItemsCount { get; set; }
        public int TotalCount { get; set; }
        public int Count => TotalCount;
        public IEnumerable<TDTO>? Data { get; set; }

    }
}
