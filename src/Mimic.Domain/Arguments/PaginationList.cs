using System.Collections.Generic;

namespace Mimic.Domain.Arguments
{
    public class PaginationList<T>
    {
        public List<T> Results { get; set; } = new List<T>();
        public Pagination Pagination { get; set; }
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
