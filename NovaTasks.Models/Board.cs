using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaTasks.Models;

[Table("boards")]
public class Board
{
    [Key]
    [Column("board_id")]
    public int BoardId { get; set; }

    [Column("workspace_id")]
    public int WorkspaceId { get; set; }

    [Required]
    [Column("name")]
    [MaxLength(200)]
    public string Name { get; set; }

    // Навигационные свойства
    [ForeignKey("WorkspaceId")]
    public Workspace Workspace { get; set; }
    public List<TasksList> TasksLists { get; set; } = new();
}