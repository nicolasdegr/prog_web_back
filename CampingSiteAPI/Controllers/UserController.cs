using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    //context db
    private readonly LiteDbContext _context;

    //context initialiseren
    public UserController(LiteDbContext context)
    {
        _context = context;
    }

    //register
    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        var existingUser = _context.Users.FindOne(u => u.Email == user.Email);
        if (existingUser != null)
        {
            return BadRequest(new { message = "Email is already registered" });
        }

        _context.Users.Insert(user);
        return Ok(new { message = "User registered successfully" });
    }
    //login
    [HttpPost("login")]
    public IActionResult Login([FromBody] User loginUser)
    {
        var user = _context.Users.FindOne(u => u.Email == loginUser.Email && u.Password == loginUser.Password);
        if (user == null)
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }

        return Ok(user);
    }
    //profile gegevens
    [HttpGet("profile")]
    public IActionResult GetProfile([FromHeader] string username)
    {
        var user = _context.Users.FindOne(u => u.Username == username);
        if (user == null) return NotFound();
        return Ok(user);
    }
    //update profile
    [HttpPut("profile")]
    public IActionResult UpdateProfile([FromBody] User updatedUser)
    {
        var existingUser = _context.Users.FindOne(u => u.Username == updatedUser.Username);
        if (existingUser == null) return NotFound(new { message = "User not found" });

        existingUser.Email = updatedUser.Email;
        existingUser.Password = updatedUser.Password;
        existingUser.Role = updatedUser.Role;

        _context.Users.Update(existingUser);
        return Ok(existingUser);
    }

}
