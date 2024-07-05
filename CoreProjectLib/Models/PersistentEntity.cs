
using Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public abstract class PersistentEntity : BaseEntity
    {
        [Required]
        public EntityState State { get; set; } = EntityState.Active;
    }
}
