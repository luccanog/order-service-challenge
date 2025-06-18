using Ambev.Models;
using Ambev.Storage.Entities;

namespace Ambev.DataTransferObjects
{
    public record OrderDetailsDto
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalValue { get; set; }
        public IEnumerable<ProductDto> Products { get; set; } 
    }
}
