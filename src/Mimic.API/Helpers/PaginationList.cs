using Mimic.WebApi.V1.Models.Dtos;
using System.Collections.Generic;

namespace Mimic.WebApi.Helpers
{
    public class PaginationList<T>
    {
        public List<T> Results { get; set; } = new List<T>();
        public Pagination Pagination { get; set; }
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
