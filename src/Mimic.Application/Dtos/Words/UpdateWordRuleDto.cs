namespace Mimic.Application.Dtos.Words
{
    public class UpdateWordRuleDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
        public bool Active { get; set; }
    }
}
