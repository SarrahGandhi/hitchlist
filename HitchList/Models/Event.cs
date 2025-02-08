using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HitchList.Models;
public class Event
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(255)]
    public string? Name { get; set; }

    public DateTime? Date { get; set; }

    [StringLength(255)]
    public string? Location { get; set; }
    [Column(TypeName = "varchar(10)")] // Ensures storage as a string
    [JsonConverter(typeof(JsonStringEnumConverter))] // Enables JSON serialization as a string
    public EventCategory Category { get; set; }

}
public enum EventCategory
{
    Bride,
    Groom,
    Both
}
public class EventDto
{
    public int EventId { get; set; }
    public string? Name { get; set; }
    public DateTime Date { get; set; }
    public string? Location { get; set; }
    [Column(TypeName = "varchar(10)")] // Ensures storage as a string
    [JsonConverter(typeof(JsonStringEnumConverter))] // Enables JSON serialization as a string
    public EventCategory Category { get; set; }
}