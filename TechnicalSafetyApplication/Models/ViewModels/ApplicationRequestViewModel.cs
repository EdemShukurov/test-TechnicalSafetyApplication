using Microsoft.AspNetCore.Mvc;

namespace TechnicalSafetyApplication.Models.ViewModels
{
    //[Bind(Include = "Application")]
    public class ApplicationRequestViewModel
    {
        public Application Application { get; set; }

        public Reply Request { get; set; }
    }
}
