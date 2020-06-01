using System.Collections.Generic;

namespace TechnicalSafetyApplication.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Application> ApplicationsEnumerator { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
