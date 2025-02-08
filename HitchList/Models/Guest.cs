using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HitchList.Models;
public enum GuestCategory
{
    Bride,
    Groom,
    Both
}
public class Guest
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(255)]
    public string? Name { get; set; }
    [Required]

    [Column(TypeName = "tinyint(1)")]
    public bool InvitationStatus { get; set; }

    [Column(TypeName = "varchar(10)")] // Ensures storage as a string
    [JsonConverter(typeof(JsonStringEnumConverter))] // Enables JSON serialization as a string
    public GuestCategory Category { get; set; }


}
public class GuestDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool InvitationStatus { get; set; }
    [Column(TypeName = "varchar(10)")] // Ensures storage as a string
    [JsonConverter(typeof(JsonStringEnumConverter))] // Enables JSON serialization as a string
    public GuestCategory Category { get; set; }
}
