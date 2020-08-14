using System.Collections.Generic;

namespace Mimic.WebApi.Helpers
{
    public class PaginationList<T> : List<T>
    {
        public Pagination Pagination { get; set; }
    }
}
