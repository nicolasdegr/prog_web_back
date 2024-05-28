using LiteDB;

public class Campground
{
    [BsonId] //auto id toeveogen
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Location { get; set; }
    public required string Description { get; set; }
    public required string Price { get; set; }
    public required List<string> Photos { get; set; }
    public required List<string> Amenities { get; set; }

}
