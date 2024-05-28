using LiteDB;

public class LiteDbContext
{
    //eigenschappen van de lb 
    public LiteDatabase Database { get; }

    // files die verbinden en schrijven
    public LiteDbContext()
    {
        Database = new LiteDatabase(@"Filename=CampingSite.db;Connection=shared;");
    }

    //toegang tot de collections van de database
    public ILiteCollection<User> Users => Database.GetCollection<User>("users");
    public ILiteCollection<Campground> Campgrounds => Database.GetCollection<Campground>("campgrounds");
    public ILiteCollection<Booking> Bookings => Database.GetCollection<Booking>("bookings");
    public ILiteCollection<Review> Reviews => Database.GetCollection<Review>("reviews");
    public ILiteCollection<Amenity> Amenities => Database.GetCollection<Amenity>("amenities");
}
