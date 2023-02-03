using System.ComponentModel.DataAnnotations;

namespace NewKnowledgeManager.Domain.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; private set; } 
    }
}
