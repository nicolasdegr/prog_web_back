using LiteDB;

public class Review
{
    [BsonId] //auto id toeveogen
    public int Id { get; set; }
    public int CampgroundId { get; set; }
    public int UserId { get; set; }
    public required string Comment { get; set; }
    public int Rating { get; set; } // 1 tot 5
}
