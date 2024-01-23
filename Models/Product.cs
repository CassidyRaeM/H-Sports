namespace H_Sports.Models
{
    public class Product
    {
        public int? Id { get; set; }
        public string? ProductName { get; set; }

        public int? SportId { get; set; }

        public float? Price { get; set;}

        public int? Inventory {  get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
