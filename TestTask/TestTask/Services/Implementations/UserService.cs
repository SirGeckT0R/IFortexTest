using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> GetUser()
        {
            User user = await _context.Users.AsNoTracking()
                                                .Include(x => x.Orders)
                                                .OrderByDescending(x => x.Orders
                                                                        .Where(o => o.CreatedAt.Year == 2003 && o.Status == OrderStatus.Delivered)
                                                                        .Sum(o => o.Quantity * o.Price))
                                                .FirstAsync();
            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            List<User> users = await _context.Users.AsNoTracking()
                                                        .Include(x => x.Orders)
                                                        .Where(x => x.Orders.Any(o => o.CreatedAt.Year == 2010 && o.Status == OrderStatus.Paid))
                                                        .ToListAsync();
            return users;
        }
    }
}