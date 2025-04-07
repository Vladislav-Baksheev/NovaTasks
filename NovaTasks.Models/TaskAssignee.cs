using System.ComponentModel.DataAnnotations.Schema;

namespace NovaTasks.Models;

[Table("task_assignees")]
public class TaskAssignee
{
    [Column("task_id")]
    public int TaskId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    // Навигационные свойства
    public Task Task { get; set; }
    public User User { get; set; }
}