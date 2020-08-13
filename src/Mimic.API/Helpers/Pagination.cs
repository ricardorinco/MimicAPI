namespace Mimic.WebApi.Helpers
{
    public class Pagination
    {
        public int Number { get; set; }
        public int Total { get; set; }

        public int DataPerPage { get; set; }
        public int TotalData { get; set; }
    }
}
