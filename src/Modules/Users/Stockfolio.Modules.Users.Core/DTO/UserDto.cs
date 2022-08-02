namespace Stockfolio.Modules.Users.Core.DTO;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public string State { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool EmailConfirmed { get; set; }
}