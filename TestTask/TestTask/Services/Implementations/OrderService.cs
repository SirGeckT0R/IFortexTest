using Microsoft.EntityFrameworkCore;
using TestTask.Data;
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
            Order order = await _context.Orders.Include(x => x.User).FirstAsync();
            return order;
        }

        public async Task<List<Order>> GetOrders()
        {
            List<Order> orders = await _context.Orders.Include(x => x.User).Where(x => x.Id > 0).ToListAsync();
            return orders;
        }
    }
}