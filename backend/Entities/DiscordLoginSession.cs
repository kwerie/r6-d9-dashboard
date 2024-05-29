using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities;

[Table(name: "discord_login_sessions")]
public class DiscordLoginSession
{
    public int Id { get; private set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [MaxLength(255)]
    public required string AccessToken { get; set; }

    [MaxLength(50)]
    public string TokenType { get; set; } = "Bearer";

    public required DateTime ExpiresIn { get; set; }

    [MaxLength(255)]
    public required string RefreshToken { get; set; }

    [MaxLength(100)]
    public required string Scope { get; set; }

    public DateTime? InvalidatedAt { get; set; } = null;
}