using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechnicalSafetyApplication.Models;
using TechnicalSafetyApplication.Models.ViewModels;

namespace TechnicalSafetyApplication.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> _userManager;

        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_userManager.Users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel createViewModel)
        {
            if(ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = createViewModel.Name,
                    Email = createViewModel.Email
                };

                IdentityResult result = await _userManager.CreateAsync(user, createViewModel.Password);

                if(result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(createViewModel);
        }
    }
}