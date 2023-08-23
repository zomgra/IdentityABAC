namespace Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public string Scopes { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
