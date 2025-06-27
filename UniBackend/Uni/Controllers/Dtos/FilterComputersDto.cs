namespace UniBackend.Controllers.Dtos
{
    public class FilterComputersDto
    {
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
