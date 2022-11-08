namespace PersonManagerService.Domain.Models;

public sealed class User
{
    public string Username { get; set; } 
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
}

