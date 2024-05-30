using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities;

[Table(name: "user_login_sessions")]
public class LoginSession
{
    public int Id { get; private set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [MaxLength(65535)]
    public string AccessToken { get; set; }

    [MaxLength(65535)]
    public string RefreshToken { get; set; }

    public DateTime ExpiresAt { get; set; }
    public DateTime IssuedAt { get; set; }
    public DateTime? InvalidatedAt { get; set; }

    private const int DefaultRefreshTokenExpiresInDays = 7;

    public LoginSession()
    {
        Refresh();
    }

    public void Refresh()
    {
        IssuedAt = DateTime.Now;
        ExpiresAt = DateTime.Now.AddDays(DefaultRefreshTokenExpiresInDays);
        RefreshToken = Guid.NewGuid().ToString();
    }
}