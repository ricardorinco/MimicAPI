using System;

namespace Mimic.WebApi.Helpers
{
    public class WordUrlQuery
    {
        public DateTime? SearchDate { get; set; }
        public int? Page { get; set; }
        public int? DataAmount { get; set; }
    }
}
