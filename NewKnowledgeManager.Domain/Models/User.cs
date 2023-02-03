using NewKnowledgeManager.Domain.Enums;

namespace NewKnowledgeManager.Domain.Models
{
    public class User : BaseEntity
    {
        public User(string login,
                    Role role)
        {
            this.ValidaUser(login, role);
            Login = login;
        }

        public string Login { get; private set; }
        public Role Role { get; private set; }

        public void ValidaUser(string login,
                               Role role)
        {
            if (string.IsNullOrEmpty(login))
                throw new InvalidOperationException(
                    "O login é inválido"
            );

            else if (Enum.IsDefined(typeof(Role), role))
                 throw new InvalidOperationException(
                    "A role é inválida"
            );
        }
    }
}
