using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HitchList.Models;
public enum AdminCategory
{
    Bride,
    Groom,
    Both
}

public class Admin
{
    [Key]
    public int User_Id { get; set; }
    [Required]
    [StringLength(50)]
    public string? Username { get; set; }
    [Required]
    [StringLength(255)]
    public string? Password_hash { get; set; }
    [Required]
    [StringLength(15)]
    public string? User_phone { get; set; } // Ideally, this should be hashed for security
    [Column(TypeName = "varchar(10)")] // Ensures storage as a string
    [JsonConverter(typeof(JsonStringEnumConverter))] // Enables JSON serialization as a string
    public AdminCategory Category { get; set; }

}
public class AdminDto
{
    public int User_Id { get; set; }
    public string? Username { get; set; }
    public string? Password_hash { get; set; }
    public string? User_phone { get; set; } // Ideally, this should be hashed for security
    [Column(TypeName = "varchar(10)")] // Ensures storage as a string
    [JsonConverter(typeof(JsonStringEnumConverter))] // Enables JSON serialization as a string
    public AdminCategory Category { get; set; }
}
