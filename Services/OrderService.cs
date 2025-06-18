using Ambev.Controllers;
using Ambev.DataTransferObjects;
using Ambev.Storage;
using Ambev.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderService> _logger;

        public OrderService(AppDbContext context, ILogger<OrderService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<OrderDetailsDto[]> GetAsync(int skip, int take, CancellationToken cancellationToken)
        {
            var all = _context.Orders.ToList();

            var result = await _context.Orders
                .Skip(skip)
                .Take(take)
                .Include(o => o.Products)
                .Select(o => new OrderDetailsDto
                {
                    Id = o.Id,
                    Products = o.Products.Select(p => new ProductDto { Name = p.Name, Value = p.Value }),
                    Date = o.Date,
                    Status = o.Status,
                    TotalValue = o.Products.Sum(p => p.Value)
                })
                .ToArrayAsync(cancellationToken);

            return result;
        }

        public async Task ProcessOrderAsync(ProcessOrderDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _context.Orders.AddAsync(new Order
                {
                    Id = dto.Id,
                    Date = dto.Date,
                    Status = dto.Status,
                }, cancellationToken);


                await _context.Products.AddRangeAsync(dto.Products.Select(p =>
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = p.Name,
                    OrderId = dto.Id,
                    Value = p.Value
                }), cancellationToken);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ex.Message, ex.StackTrace);

                throw;
            }
        }
    }
}
