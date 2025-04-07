using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaTasks.Models;

[Table("tasks_list")]
public class TasksList
{
    [Key]
    [Column("tasks_list_id")]
    public int TasksListId { get; set; }

    [Column("board_id")]
    public int BoardId { get; set; }

    [Required]
    [Column("name")]
    [MaxLength(200)]
    public string Name { get; set; }

    // Навигационные свойства
    [ForeignKey("BoardId")]
    public Board Board { get; set; }
    public List<Task> Tasks { get; set; } = new();
}