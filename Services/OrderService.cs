using RetroVideoStore.Data;
using RetroVideoStore.Models;
using Microsoft.EntityFrameworkCore;

namespace RetroVideoStore.Services
{
    public class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<List<Order>> GetOrdersAsync()
        {
            return await _context.Orders.Include(o => o.OrderDetails)
                .ThenInclude(od => od.Movie)
                .ToListAsync();
        }
        public async Task<Order> GetOrderByOrderIdAsync(Guid orderID)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Movie)
                .FirstOrDefaultAsync(o => o.OrderID == orderID);

            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderID} not found.");
            }

            return order;
        }
    }
}