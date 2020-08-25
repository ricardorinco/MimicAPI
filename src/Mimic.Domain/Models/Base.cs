using System;

namespace Mimic.Domain.Models
{
    public abstract class Base
    {
        public int Id { get; set; }
        public bool Active { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
