using LiteDB;

public class Amenity
{
    [BsonId] //auto id toevoegen
    public int Id { get; set; }
    public required string Name { get; set; }
}
