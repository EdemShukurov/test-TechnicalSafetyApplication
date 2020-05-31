using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalSafetyApplication.Models
{

    public class Reply
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? ModificationTime { get; set; }


        [ForeignKey(nameof(AppUser))]
        public string UserId { get; set; }

        public AppUser User { get; set; }
    }
}
