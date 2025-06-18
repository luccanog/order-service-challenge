namespace Ambev.Storage.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
