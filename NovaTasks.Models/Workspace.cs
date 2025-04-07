using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaTasks.Models;

[Table("workspaces")]
public class Workspace
{
    [Key]
    [Column("workspace_id")]
    public int WorkspaceId { get; set; }

    [Required]
    [Column("name")]
    [MaxLength(200)]
    public string Name { get; set; }

    [Column("visibility")]
    [MaxLength(10)]
    public string Visibility { get; set; } = "private"; // private/public

    // Навигационные свойства
    public List<Board> Boards { get; set; } = new();
    public List<WorkspaceMember> Members { get; set; } = new();
}