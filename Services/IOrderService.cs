using Ambev.DataTransferObjects;
using Ambev.Storage.Entities;

namespace Ambev.Services
{
    public interface IOrderService
    {
        Task ProcessOrderAsync(ProcessOrderDto dto, CancellationToken cancellationToken);
        Task<OrderDetailsDto[]> GetAsync(int skip, int take, CancellationToken cancellationToken);
    }
}