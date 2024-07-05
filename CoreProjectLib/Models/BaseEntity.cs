using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? LastModifiedDate { get; set; }
    }
}
