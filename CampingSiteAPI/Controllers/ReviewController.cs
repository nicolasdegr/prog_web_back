using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ReviewController : ControllerBase
{
    //context db
    private readonly LiteDbContext _context;

    //context intialiseren
    public ReviewController(LiteDbContext context)
    {
        _context = context;
    }

    //toevoegen review op basis van username en camping en rating, ...
    [HttpPost("add")]
    public IActionResult AddReview([FromQuery] string campgroundName, [FromQuery] string username, [FromBody] Review review)
    {
        var user = _context.Users.FindOne(u => u.Username == username);
        if (user == null) return NotFound(new { message = "User not found" });

        var campground = _context.Campgrounds.FindOne(c => c.Name == campgroundName);
        if (campground == null) return NotFound(new { message = "Campground not found" });

        review.UserId = user.Id;
        review.CampgroundId = campground.Id;
        _context.Reviews.Insert(review);

        return Ok(review);
    }
    // verkrijg alle reviews
    [HttpGet("list")]
    public IActionResult GetReviews([FromQuery] string campgroundName)
    {
        var campground = _context.Campgrounds.FindOne(c => c.Name == campgroundName);
        if (campground == null) return NotFound(new { message = "Campground not found" });

        var reviews = _context.Reviews.Find(r => r.CampgroundId == campground.Id).ToList();
        return Ok(reviews);
    }
    //update review (niet relevant)
    [HttpPut("update")]
    public IActionResult UpdateReview([FromQuery] int reviewId, [FromBody] Review updatedReview)
    {
        var review = _context.Reviews.FindById(reviewId);
        if (review == null) return NotFound(new { message = "Review not found" });

        updatedReview.Id = reviewId;
        _context.Reviews.Update(updatedReview);
        return Ok(updatedReview);
    }
    //delete review (niet relevant)
    [HttpDelete("delete")]
    public IActionResult DeleteReview([FromQuery] int reviewId)
    {
        var review = _context.Reviews.FindById(reviewId);
        if (review == null) return NotFound(new { message = "Review not found" });

        _context.Reviews.Delete(reviewId);
        return Ok(new { message = "Review deleted" });
    }

}
