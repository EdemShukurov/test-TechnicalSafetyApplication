namespace TechnicalSafetyApplication.Models
{
    public enum NotificationType : byte
    {
        Created, 
        Modified
    }

    public class Notification
    {
        public int Id { get; set; }

        public NotificationType Type { get; set; }

        // Foreign keys
        public int UserId { get; set; }

        public User User { get; set; }

        public int ApplicationId { get; set; }

        public Application Application { get; set; }
    }
}
