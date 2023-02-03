using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewKnowledgeManager.Domain.Interfaces;
using NewKnowledgeManager.Domain.Models;

namespace NewKnowledgeManager.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Context.CustomDbContext _context;

        public UserRepository(Context.CustomDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByLogin(string login)
        {
            return await _context.Users.Where(u =>
                                              u.Login == login)
                                       .FirstOrDefaultAsync();

        }
        
        public async Task<ICollection<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();

        }
    }
}
