using System;
using System.ComponentModel.DataAnnotations.Schema;

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


        [ForeignKey(nameof(AppUser))]
        public string UserId { get; set; }

        public AppUser User { get; set; }

        public int? ReplyId { get; set; }

        public Application()
        {
            this.CreationTime = DateTime.UtcNow;
            this.ModificationTime = DateTime.UtcNow;
        }
    }
}
