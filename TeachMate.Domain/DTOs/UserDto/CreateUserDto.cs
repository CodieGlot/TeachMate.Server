namespace TeachMate.Domain;
public class CreateUserDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Email { get; set; }
    public UserRole UserRole { get; set; } = UserRole.Learner;
}
