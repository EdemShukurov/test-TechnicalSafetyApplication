﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TechnicalSafetyApplication.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View(new Dictionary<string, object> { ["PlaceHolder"] = "PlaceHolder"});
        }
    }
}