namespace Store.G02.Domain.Entities.Identity
{
    public class Address
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int Id { get; set; }
        public string Country { get; set; }

        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
    }
}