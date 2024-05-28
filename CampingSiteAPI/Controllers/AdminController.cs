using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly LiteDbContext _context;

    // Constructor voor de databasee context te initialiseren
    public AdminController(LiteDbContext context)
    {
        _context = context;
    }

    [HttpGet("dashboard")]
    public IActionResult GetDashboard()
    {
        // Implementeer logica om dashboard overzicht op te halen
        return Ok(new { Users = _context.Users.Count(), Campgrounds = _context.Campgrounds.Count(), Bookings = _context.Bookings.Count(), Reviews = _context.Reviews.Count() });
    }
    // Haal alle gebruikers op
    [HttpGet("users")]
    public IActionResult GetUsers()
    {
        var users = _context.Users.FindAll().ToList();
        return Ok(users);
    }
    // Haal een specifieke gebruiker op basis van de gebruikersnaam
    [HttpGet("user")]
    public IActionResult GetUser([FromQuery] string username)
    {
        var user = _context.Users.FindOne(u => u.Username == username);
        if (user == null) return NotFound();
        return Ok(user);
    }
    // update een specifieke gebruiker op basis van de gebruikersnaam
    [HttpPut("user/update")]
    public IActionResult UpdateUser([FromQuery] string username, User updatedUser)
    {
        var user = _context.Users.FindOne(u => u.Username == username);
        if (user == null) return NotFound();

        updatedUser.Id = user.Id;
        _context.Users.Update(updatedUser);
        return Ok(updatedUser);
    }
    // Verwijder een specifieke gebruiker op basis van de gebruikersnaam
    [HttpDelete("user/delete")]
    public IActionResult DeleteUser([FromQuery] string username)
    {
        var user = _context.Users.FindOne(u => u.Username == username);
        if (user == null) return NotFound();

        _context.Users.Delete(user.Id);
        return Ok();
    }
    // Haal alle reviews op
    [HttpGet("reviews")]
    public IActionResult GetReviews()
    {
        var reviews = _context.Reviews.FindAll().ToList();
        return Ok(reviews);
    }
    //delete review
    [HttpDelete("review/delete")]
    public IActionResult DeleteReview([FromQuery] int reviewId)
    {
        var review = _context.Reviews.FindById(reviewId);
        if (review == null) return NotFound();

        _context.Reviews.Delete(reviewId);
        return Ok();
    }
    //toeveoegen amenity
    [HttpPost("amenity/add")]
    public IActionResult AddAmenity(Amenity amenity)
    {
        _context.Amenities.Insert(amenity);
        return Ok(amenity);
    }
    //update van amenity
    [HttpPut("amenity/update")]
    public IActionResult UpdateAmenity([FromQuery] int amenityId, Amenity updatedAmenity)
    {
        var amenity = _context.Amenities.FindById(amenityId);
        if (amenity == null) return NotFound();

        updatedAmenity.Id = amenityId;
        _context.Amenities.Update(updatedAmenity);
        return Ok(updatedAmenity);
    }
    //Delete Amenity
    [HttpDelete("amenity/delete")]
    public IActionResult DeleteAmenity([FromQuery] int amenityId)
    {
        var amenity = _context.Amenities.FindById(amenityId);
        if (amenity == null) return NotFound();

        _context.Amenities.Delete(amenityId);
        return Ok();
    }
}
