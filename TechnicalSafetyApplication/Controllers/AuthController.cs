using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechnicalSafetyApplication.Controllers
{
    public class AuthController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View(new Dictionary<string, object> { ["PlaceHolder"] = "PlaceHolder"});
        }
    }
}