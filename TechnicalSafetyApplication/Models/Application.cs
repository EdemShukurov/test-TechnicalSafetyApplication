using System;

namespace TechnicalSafetyApplication.Models
{
    public enum Status : byte
    {
        Sent,
        OnConsideration,
        Processed
    }

    public class Application
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public string Theme { get; set; }

        public Status CurrentStatus { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? ModificationTime { get; set; }


        // Foreign keys
        public int UserId { get; set; }

        public User User { get; set; }

        public int ReplyId { get; set; }

        public Reply Reply { get; set; }
    }
}
