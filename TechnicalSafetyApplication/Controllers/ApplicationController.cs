using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechnicalSafetyApplication.Models.Repository.Interfaces;

namespace TechnicalSafetyApplication.Controllers
{
    public class ApplicationController : Controller
    {
        private IApplicationRepository _repository;

        public ApplicationController(IApplicationRepository repository)
        {
            _repository = repository;
        }

        public IActionResult List()
        {
            return View(_repository.Applications);
        }
    }
}