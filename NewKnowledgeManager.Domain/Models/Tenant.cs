using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewKnowledgeManager.Domain.Models
{
    public class Tenant : BaseEntity
    {
        public Tenant(string name, string connectionStringDb)
        {
            Name = name;
            ConnectionStringDb = connectionStringDb;
        }

        public string Name { get; private set; }

        public string ConnectionStringDb { get; private set; }

        protected void ValidateTenant (string name, string connectionStringDb)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("O nome é inválido");
            if (string.IsNullOrEmpty(connectionStringDb))
                throw new InvalidOperationException("A connectionString é inválida");
        }
    }
}
