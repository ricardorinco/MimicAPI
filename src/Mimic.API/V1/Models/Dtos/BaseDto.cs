using System;
using System.Collections.Generic;

namespace Mimic.WebApi.V1.Models.Dtos
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public bool Active { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
