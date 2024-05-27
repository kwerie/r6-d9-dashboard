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

    [Column(TypeName = "text"), MaxLength(255)]
    public required string DiscordAccountId { get; set; }

    [MaxLength(255)]
    public required string Username { get; set; }

    [Column(TypeName = "text"), MaxLength(255)]
    public required string AvatarUrl { get; set; }

    [Key, MaxLength(255)]
    public required string Email { get; set; }

    public ICollection<DiscordLoginSession> DiscordLoginSessions { get; } = new List<DiscordLoginSession>();
}