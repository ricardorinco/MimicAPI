using System;

namespace Mimic.Application.Dtos.Words
{
    public class UpdateWordRequestDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
        public bool Active { get; set; }
    }
}
