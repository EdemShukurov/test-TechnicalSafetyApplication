﻿using System.ComponentModel.DataAnnotations.Schema;

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


        [ForeignKey(nameof(AppUser))]
        public string UserId { get; set; }
        public AppUser User { get; set; }


        [ForeignKey(nameof(Application))]
        public int ClaimId { get; set; }

        public Application Claim { get; set; }
    }
}
