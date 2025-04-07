using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaTasks.Models;

[Table("labels")]
public class Label
{
    [Key]
    [Column("label_id")]
    public int LabelId { get; set; }

    [Required]
    [Column("name")]
    [MaxLength(50)]
    public string Name { get; set; }

    [Column("color")]
    [MaxLength(7)]
    public string Color { get; set; } = "#FFFFFF";

    // Навигационные свойства
    public List<TaskLabel> TaskLabels { get; set; } = new();
}