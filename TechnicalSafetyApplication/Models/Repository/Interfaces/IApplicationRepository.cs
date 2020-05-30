using System.Linq;

namespace TechnicalSafetyApplication.Models.Repository.Interfaces
{
    public interface IApplicationRepository
    {
        IQueryable<Application> Applications { get; }
    }
}
