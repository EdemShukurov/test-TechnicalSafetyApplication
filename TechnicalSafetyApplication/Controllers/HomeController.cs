using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechnicalSafetyApplication.Models;

namespace TechnicalSafetyApplication.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        [Authorize]
        public IActionResult Index()
        {
            return View(GetData(nameof(Index)));
        }

        [Authorize(Roles = "Users")]
        public IActionResult OtherAction()
        {
            return View(nameof(Index), GetData(nameof(OtherAction)));
        }

        [Authorize]
        public async Task<IActionResult> UserProps()
        {
            return View(await CurrentUser);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UserProps([Required]Cities city, [Required]QualificationLevels qualifications)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await CurrentUser;

                user.City = city;
                user.Qualifications = qualifications;

                await _userManager.UpdateAsync(user);

                return RedirectToAction(nameof(Index));
            }

            return View(await CurrentUser);
        }

        private Dictionary<string, object> GetData(string actionName)
        {
            return new Dictionary<string, object>
            {
                //["Action"] = actionName,
                ["User"] = HttpContext.User.Identity.Name,
                //["Authenticated"] = HttpContext.User.Identity.IsAuthenticated,
                ["Auth Type"] = HttpContext.User.Identity.AuthenticationType,
                ["Is Employees Role"] = HttpContext.User.IsInRole(Utility.EMPLOYEES_ROLE),
                //["City"] = CurrentUser.Result.City,
                //["Qualification"] = CurrentUser.Result.Qualifications
            };
        }

        private Task<AppUser> CurrentUser
        {
            get
            {
                return _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }
        }
    }
}
