using Ambev.DataTransferObjects;
using Ambev.Services;
using Ambev.Storage.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(
            CancellationToken cancellationToken,
            [FromQuery] int skip = 0,
            [FromQuery] int take = 5)
        {
            var orders = await _orderService.GetAsync(skip, take, cancellationToken);

            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveOrderAsync([FromBody] ProcessOrderDto dto, CancellationToken cancellationToken)
        {
            try
            {
                await _orderService.ProcessOrderAsync(dto, cancellationToken);

                return NoContent();
            }
            catch (DbUpdateException)
            {
                return Conflict(string.Format("Order already {0} processed before.", dto.Id));
            }
        }
    }
}