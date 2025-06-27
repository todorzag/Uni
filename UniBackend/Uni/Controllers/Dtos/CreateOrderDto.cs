namespace UniBackend.Controllers.Dtos
{
    public class CreateOrderDto
    {
        public string OrderAddress { get; set; } = null!;
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}
