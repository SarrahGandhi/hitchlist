using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HitchList.Models;
public enum TaskCategory
{
    Bride,
    Groom,
    Both
}
public class Task
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(255)]
    public string TaskName { get; set; }
    [Required]
    [StringLength(255)]
    public string TaskDescription { get; set; }
    [Required]
    public DateTime TaskDueDate { get; set; }
    [ForeignKey("Event")]
    public int EventId { get; set; }
    public virtual Event Event { get; set; }
    [ForeignKey("Admin")]
    public int User_Id { get; set; }
    public virtual Admin Admin { get; set; }
    [Required]
    public bool TaskStatus { get; set; }
    [Column(TypeName = "varchar(10)")] // Ensures storage as a string
    [JsonConverter(typeof(JsonStringEnumConverter))] // Enables JSON serialization as a string

    public TaskCategory TaskCategory { get; set; }

}
public class TaskDto
{
    public int Id { get; set; }
    public string? TaskName { get; set; }
    public string? TaskDescription { get; set; }
    public DateTime TaskDate { get; set; }
    public bool TaskStatus { get; set; }
    [Column(TypeName = "varchar(10)")] // Ensures storage as a string
    [JsonConverter(typeof(JsonStringEnumConverter))] // Enables JSON serialization as a string
    public TaskCategory TaskCategory { get; set; }

}

