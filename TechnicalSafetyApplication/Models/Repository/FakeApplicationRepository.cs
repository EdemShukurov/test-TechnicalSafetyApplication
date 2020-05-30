using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalSafetyApplication.Models.Repository.Interfaces;

namespace TechnicalSafetyApplication.Models.Repository
{
    public class FakeApplicationRepository : IApplicationRepository
    {
        public IQueryable<Application> Applications =>
            new List<Application>
            {
                new Application {Message = "Problem #1", Theme ="Theme #1"},
                new Application {Message = "Problem #2", Theme ="Theme #2"},

            }.AsQueryable<Application>();
    }
}
