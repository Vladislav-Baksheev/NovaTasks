using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaTasks.Models;

[Table("workspace_members")]
public class WorkspaceMember
{
    [Column("workspace_id")]
    public int WorkspaceId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("role")]
    [MaxLength(10)]
    public string Role { get; set; } = "member"; // admin/member

    // Навигационные свойства
    public Workspace Workspace { get; set; }
    public User User { get; set; }
}