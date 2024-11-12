using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrder()
        {
            Order order = await _context.Orders.AsNoTracking()
                                                    .Include(x => x.User)
                                                    .OrderByDescending(x => x.CreatedAt)
                                                    .FirstAsync(x => x.Quantity > 1);
            return order;
        }

        public async Task<List<Order>> GetOrders()
        {
            List<Order> orders = await _context.Orders.AsNoTracking()
                                                            .Include(x => x.User)
                                                            .Where(x => x.User.Status == UserStatus.Active)
                                                            .OrderByDescending(x => x.CreatedAt)
                                                            .ToListAsync();
            return orders;
        }
    }
}