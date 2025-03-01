﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechnicalSafetyApplication.Models;
using TechnicalSafetyApplication.Models.ViewModels;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace TechnicalSafetyApplication.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl + "application";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel details, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(details.Email);
                if(user != null)
                {
                    await _signInManager.SignOutAsync();

                    SignInResult result = await _signInManager.PasswordSignInAsync(user, details.Password, false, false);

                    if(result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);

                        if (roles.Contains(Utility.ADMINS_ROLE))
                        {
                            return Redirect("/admin");
                        }
                        else
                        {
                            
                            return Redirect("/application");
                        }
                        
                    }
                }

                ModelState.AddModelError(nameof(LoginViewModel.Email), "Invalid user or password");
            }

            return View(details);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index),"Home");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index), "Home");
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        [AllowAnonymous]
        public IActionResult AccessDenies()
        {
            return View();
        }
    }
}