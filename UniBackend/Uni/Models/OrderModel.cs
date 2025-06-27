using System.ComponentModel.DataAnnotations.Schema;

namespace UniBackend.Models
{
    [Table("Order")]
    public class OrderModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string OrderAddress { get; set; } = null!;
        public decimal Total { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<OrderItemModel> Items { get; set; } = new();
    }
}
