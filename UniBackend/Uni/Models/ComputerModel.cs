using System.ComponentModel.DataAnnotations.Schema;

namespace UniBackend.Models
{
    [Table("Computer")]
    public class ComputerModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Type { get; set; } = null!;
        public string Processor { get; set; } = null!;
        public string RAM { get; set; } = null!;
        public string Storage { get; set; } = null!;
        public decimal? ScreenSize { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
