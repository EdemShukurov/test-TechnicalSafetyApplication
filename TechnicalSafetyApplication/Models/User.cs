namespace TechnicalSafetyApplication.Models
{
    public enum Role : byte
    {
        Administator,
        Employee
    }

    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int PersonnelNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Role UserRole { get; set; }
    }
}
