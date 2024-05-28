using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    //context initialiseren voor db
    private readonly LiteDbContext _context;

    //constructur contextt
    public BookingController(LiteDbContext context)
    {
        _context = context;
    }
    // Zoek campings op basis van locatie en datums
    [HttpGet("search")]
    public IActionResult SearchCampgrounds([FromQuery] string location, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var campgrounds = _context.Campgrounds.Find(c => c.Location.Contains(location)).ToList();
        return Ok(campgrounds);
    }
    // Maak een reservering
    [HttpPost("create")]
    public IActionResult MakeReservation([FromQuery] string username, [FromQuery] string campgroundName, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var user = _context.Users.FindOne(u => u.Username == username);
        if (user == null) return NotFound(new { message = "User not found" });

        var campground = _context.Campgrounds.FindOne(c => c.Name == campgroundName);
        if (campground == null) return NotFound(new { message = "Campground not found" });

        var booking = new Booking
        {
            UserId = user.Id,
            CampgroundId = campground.Id,
            StartDate = startDate,
            EndDate = endDate
        };

        _context.Bookings.Insert(booking);
        return Ok(booking);
    }
    // Haal een specifieke boeking op
    [HttpGet("detail")]
    public IActionResult GetBooking([FromQuery] string username, [FromQuery] string campgroundName)
    {
        var user = _context.Users.FindOne(u => u.Username == username);
        if (user == null) return NotFound(new { message = "User not found" });

        var campground = _context.Campgrounds.FindOne(c => c.Name == campgroundName);
        if (campground == null) return NotFound(new { message = "Campground not found" });

        var booking = _context.Bookings.FindOne(b => b.UserId == user.Id && b.CampgroundId == campground.Id);
        if (booking == null) return NotFound(new { message = "Booking not found" });

        var bookingDetail = new
        {
            BookingId = booking.Id,
            CampgroundName = campground.Name,
            CampgroundLocation = campground.Location,
            UserName = user.Username,
            UserEmail = user.Email,
            booking.StartDate,
            booking.EndDate
        };

        return Ok(bookingDetail);
    }
    // cancel een boeking
    [HttpDelete("cancel")]
    public IActionResult CancelBooking([FromQuery] string username, [FromQuery] string campgroundName)
    {
        var user = _context.Users.FindOne(u => u.Username == username);
        if (user == null) return NotFound(new { message = "User not found" });

        var campground = _context.Campgrounds.FindOne(c => c.Name == campgroundName);
        if (campground == null) return NotFound(new { message = "Campground not found" });

        var booking = _context.Bookings.FindOne(b => b.UserId == user.Id && b.CampgroundId == campground.Id);
        if (booking == null) return NotFound(new { message = "Booking not found" });

        _context.Bookings.Delete(booking.Id);
        return Ok(new { message = "Booking cancelled" });
    }
    // Werk een boeking bij (alleen voor admin)
    [HttpPut("admin/update")]
    public IActionResult UpdateBooking([FromQuery] string username, [FromQuery] string campgroundName, Booking updatedBooking)
    {
        var user = _context.Users.FindOne(u => u.Username == username);
        if (user == null) return NotFound(new { message = "User not found" });

        var campground = _context.Campgrounds.FindOne(c => c.Name == campgroundName);
        if (campground == null) return NotFound(new { message = "Campground not found" });

        var booking = _context.Bookings.FindOne(b => b.UserId == user.Id && b.CampgroundId == campground.Id);
        if (booking == null) return NotFound(new { message = "Booking not found" });

        updatedBooking.Id = booking.Id;
        updatedBooking.UserId = booking.UserId;
        updatedBooking.CampgroundId = booking.CampgroundId;

        _context.Bookings.Update(updatedBooking);
        return Ok(updatedBooking);
    }
    // Verwijder een boeking (alleen voor admin)
    [HttpDelete("admin/delete")]
    public IActionResult DeleteBooking([FromQuery] string username, [FromQuery] string campgroundName)
    {
        var user = _context.Users.FindOne(u => u.Username == username);
        if (user == null) return NotFound(new { message = "User not found" });

        var campground = _context.Campgrounds.FindOne(c => c.Name == campgroundName);
        if (campground == null) return NotFound(new { message = "Campground not found" });

        var booking = _context.Bookings.FindOne(b => b.UserId == user.Id && b.CampgroundId == campground.Id);
        if (booking == null) return NotFound(new { message = "Booking not found" });

        _context.Bookings.Delete(booking.Id);
        return Ok(new { message = "Booking deleted" });
    }
    // Maak een booking (alleen voor admin)
    [HttpPost("admin/create")]
    public IActionResult AdminMakeReservation([FromQuery] string username, [FromQuery] string campgroundName, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var user = _context.Users.FindOne(u => u.Username == username);
        if (user == null) return NotFound(new { message = "User not found" });

        var campground = _context.Campgrounds.FindOne(c => c.Name == campgroundName);
        if (campground == null) return NotFound(new { message = "Campground not found" });

        var booking = new Booking
        {
            UserId = user.Id,
            CampgroundId = campground.Id,
            StartDate = startDate,
            EndDate = endDate
        };

        _context.Bookings.Insert(booking);
        return Ok(booking);
    }
}
