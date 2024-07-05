using CoreProjectLib.Enums;
using System.ComponentModel.DataAnnotations;

namespace CoreProjectLib.Models
{
    public abstract class PersistentEntity : BaseEntity
    {
        [Required]
        public EntityState State { get; set; } = EntityState.Active;
    }
}
