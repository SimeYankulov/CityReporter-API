namespace CityReporter.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Role { get; set; } = String.Empty;
        public byte[] Salt { get; set; } = Array.Empty<byte>();
    }
}
