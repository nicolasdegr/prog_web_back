using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class CampgroundController : ControllerBase
{
    //context db
    private readonly LiteDbContext _context;

    //initialiseren context db
    public CampgroundController(LiteDbContext context)
    {
        _context = context;
    }
    // alle campgroundds
    [HttpGet("all")]
    public IActionResult GetCampgrounds()
    {
        var campgrounds = _context.Campgrounds.FindAll()
            .Select(c => new
            {
                c.Id,
                c.Name,
                c.Location,
                c.Description,
                c.Price,
                c.Photos,
                c.Amenities
            })
            .ToList();
        return Ok(campgrounds);
    }

    //get campground op basis van filter
    [HttpGet("detail")]
    public IActionResult GetCampground([FromQuery] string price, [FromQuery] string? location = null, [FromQuery] string? amenities = null)
    {
        if (!decimal.TryParse(price, out decimal parsedPrice))
        {
            return BadRequest("Invalid price format.");
        }

        var campgrounds = _context.Campgrounds.FindAll()
            .Where(c => decimal.TryParse(c.Price, out decimal campgroundPrice) && campgroundPrice <= parsedPrice)
            .ToList();

        if (!string.IsNullOrEmpty(location))
        {
            campgrounds = campgrounds
                .Where(c => c.Location.ToLower() == location.ToLower())
                .ToList();
        }

        if (!string.IsNullOrEmpty(amenities))
        {
            var amenitiesList = amenities.ToLower().Split(',').Select(a => a.Trim()).ToList();
            campgrounds = campgrounds
                .Where(c => c.Amenities.Any(a => amenitiesList.Contains(a.ToLower())))
                .ToList();
        }

        if (!campgrounds.Any()) return NotFound();

        return Ok(campgrounds);
    }
    //campground via naam
    [HttpGet("detailByName")]
    public IActionResult GetCampgroundByName([FromQuery] string name)
    {
        var campground = _context.Campgrounds.FindOne(c => c.Name == name);
        if (campground == null) return NotFound();
        return Ok(campground);
    }
    //toeveogen campground (admin only)
    [HttpPost("admin/add")]
    public IActionResult AddCampground(Campground campground)
    {
        _context.Campgrounds.Insert(campground);
        return Ok(campground);
    }
    //update campground (admin only)
    [HttpPut("admin/update")]
    public IActionResult UpdateCampground([FromQuery] string name, [FromBody] Campground updatedCampground)
    {
        var campground = _context.Campgrounds.FindOne(c => c.Name == name);
        if (campground == null) return NotFound();

        campground.Name = updatedCampground.Name;
        campground.Location = updatedCampground.Location;
        campground.Description = updatedCampground.Description;
        campground.Price = updatedCampground.Price;
        campground.Photos = updatedCampground.Photos;
        campground.Amenities = updatedCampground.Amenities;

        _context.Campgrounds.Update(campground);
        return Ok(campground);
    }
    //Delete Campground (admin only)
    [HttpDelete("admin/delete")]
    public IActionResult DeleteCampground([FromQuery] string name)
    {
        var campground = _context.Campgrounds.FindOne(c => c.Name == name);
        if (campground == null) return NotFound();

        _context.Campgrounds.Delete(campground.Id);
        return Ok();
    }
}
