using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaTasks.Models;

[Table("tasks")]
public class Task
{
    [Key]
    [Column("task_id")]
    public int TaskId { get; set; }

    [Column("tasks_list_id")]
    public int TasksListId { get; set; }

    [Required]
    [Column("title")]
    [MaxLength(200)]
    public string Title { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("is_complete")]
    public bool IsComplete { get; set; }

    [Column("start_date")]
    public DateTime? StartDate { get; set; }

    [Column("due_date")]
    public DateTime? EndDate { get; set; }

    [Column("color")]
    [MaxLength(7)]
    public string Color { get; set; } = "#FFFFFF";

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Навигационные свойства
    [ForeignKey("TasksListId")]
    public TasksList TasksList { get; set; }
    public List<TaskAssignee> TaskAssignees { get; set; } = new();
    public List<TaskLabel> TaskLabels { get; set; } = new();
}