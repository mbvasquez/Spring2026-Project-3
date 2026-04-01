using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Spring2026_Project3_mbvasquez.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public int Age { get; set; }
        public string? ImbdLink { get; set; }
        public byte[]? Photo { get; set; }
    }
}
