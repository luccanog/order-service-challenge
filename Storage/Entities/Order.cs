using Ambev.Models;

namespace Ambev.Storage.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public OrderStatus Status { get; set; }

        public IEnumerable<Product> Products { get; set; }

    }
}