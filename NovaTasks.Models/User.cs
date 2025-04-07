using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaTasks.Models;

[Table("users")]
public class User
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    [Required]
    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [Column("email")]
    [MaxLength(255)]
    public string Email { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Навигационные свойства
    public List<WorkspaceMember> WorkspaceMemberships { get; set; } = new();
    public List<TaskAssignee> AssignedTasks { get; set; } = new();
}