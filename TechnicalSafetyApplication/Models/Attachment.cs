using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalSafetyApplication.Models
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }

        [Column("nvarchar(50)")]
        public string Title { get; set; }

        [DisplayName("File name")]
        public string Name { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile FormFile { get; set; }

        // foreign key
        public int ClaimId { get; set; }

        public Application Claim { get; set; }
    }
}
