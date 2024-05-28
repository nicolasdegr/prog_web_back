using LiteDB;

public class Booking
{
    [BsonId] //auto id toevoegen
    public int Id { get; set; }
    public int CampgroundId { get; set; }
    public int UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
