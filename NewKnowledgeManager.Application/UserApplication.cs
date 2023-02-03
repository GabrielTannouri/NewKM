using NewKnowledgeManager.Domain.Interfaces;
using NewKnowledgeManager.Domain.Models;

namespace NewKnowledgeManager.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepository _userRepository;

        public UserApplication(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByLogin(string login)
        {
            return await _userRepository.GetUserByLogin(login);
        }
        
        public async Task<ICollection<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }
    }
}
