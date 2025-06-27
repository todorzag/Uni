using System.ComponentModel.DataAnnotations.Schema;

namespace UniBackend.Models
{
    [Table("OrderItem")]
    public class OrderItemModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ComputerId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
