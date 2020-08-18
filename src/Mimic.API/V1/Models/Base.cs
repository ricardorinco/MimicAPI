using System;

namespace Mimic.WebApi.V1.Models
{
    public abstract class Base
    {
        public int Id { get; set; }
        public bool Active { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
