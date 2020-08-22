using System;

namespace Mimic.Domain.Arguments
{
    public class WordQuery
    {
        public DateTime? SearchDate { get; set; }
        public int? Page { get; set; } = 1;
        public int? DataAmount { get; set; } = 10;
    }
}
