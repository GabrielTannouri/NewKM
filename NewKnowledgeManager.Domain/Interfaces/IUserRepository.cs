using NewKnowledgeManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewKnowledgeManager.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByLogin(string login);
        Task<ICollection<User>> GetUsers();
    }
}
