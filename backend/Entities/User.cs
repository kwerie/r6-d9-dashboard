using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backend.Entities;

[Table(name: "users")]
[PrimaryKey("Id")]
[Index("Email", IsUnique = true)]
public class User
{
    public int Id { get; private set; }
    [Column(TypeName = "text")] public string DiscordAccountId { get; set; }
    [MaxLength(255)] public string Username { get; set; }
    [Column(TypeName = "text")] public string AvatarUrl { get; set; }
    [Key, MaxLength(255)] public string Email { get; set; }
}