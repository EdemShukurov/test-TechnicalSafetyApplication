using System;

namespace TechnicalSafetyApplication.Models
{

    public class Reply
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? ModificationTime { get; set; }


        // Foreign key to user
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
