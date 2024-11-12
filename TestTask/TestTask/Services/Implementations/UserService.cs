using Microsoft.EntityFrameworkCore;
using TestTask.Data;
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
            User user = await _context.Users.Include(x=>x.Orders).FirstAsync();
            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            List<User> users = await _context.Users.Include(x => x.Orders).Where(x => x.Id > 0).ToListAsync();
            return users;
        }
    }
}