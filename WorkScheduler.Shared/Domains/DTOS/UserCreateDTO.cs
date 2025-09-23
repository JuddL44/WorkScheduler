public class UserCreateDTO
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; }


    public UserCreateDTO(string username, string email, string passwordhash, Role role)
    {
        Username = username;
        Email = email;
        Password = passwordhash;
        Role = role;
    }
}
