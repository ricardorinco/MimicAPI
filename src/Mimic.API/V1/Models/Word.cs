using System.ComponentModel.DataAnnotations;

namespace Mimic.WebApi.V1.Models
{
    public class Word : Base
    {
        [Required]
        [MaxLength(150)]
        public string Mimic { get; set; }
        [Required]
        public int Points { get; set; }
    }
}
