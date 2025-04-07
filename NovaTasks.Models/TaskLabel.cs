using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace NovaTasks.Models;

[Table("task_labels")]
public class TaskLabel
{
    [Column("task_id")]
    public int TaskId { get; set; }

    [Column("label_id")]
    public int LabelId { get; set; }

    // Навигационные свойства
    public Task Task { get; set; }
    public Label Label { get; set; }
}