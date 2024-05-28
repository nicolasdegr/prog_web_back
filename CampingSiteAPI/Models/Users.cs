using LiteDB;

public class User
{
    [BsonId] //auto id
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; } // "Admin" of "User" kiezen
}
