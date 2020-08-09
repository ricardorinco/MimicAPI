using System;

namespace Mimic.WebApi.Models
{
    public abstract class Base
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
