using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TechnicalSafetyApplication.Models;
using TechnicalSafetyApplication.Models.ViewModels;

namespace TechnicalSafetyApplication.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        private string _userId;

        public ApplicationController(AppIdentityDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Application
        public async Task<IActionResult> Index(int page = 1)
        {
            var userId = new SqlParameter("@userId", User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (userId.SqlValue == null)
            {
                return RedirectToAction("LogIn", "Account", new { area = "" });
            }

            int pageSize = 3;

            _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<Application> list;

            if(User.IsInRole(Utility.MANAGERS_ROLE))
            {
                list = _context.Claims.ToList();

            }
            else
            {
                list = _context.Claims.FromSqlRaw("EXECUTE GetApplicationsByUserId @userId", userId).ToList();
            }


            PageViewModel pageViewModel = new PageViewModel(list.Count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                ApplicationsEnumerator = list.Skip((page - 1) * pageSize).Take(pageSize).ToList()
            };

            return View(viewModel);

            // var list2 = _context.Claims.Where(x => x.UserId == _userId).ToList();


            //TODO: list ?

            var appIdentityDbContext = _context.Claims.Include(a => a.User);
        }

        // GET: Application/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Claims
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Application/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Application/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Message,Theme,CurrentStatus,CreationTime,ModificationTime,ApplicationFile,UserId,ReplyId")] Application application)
        {
            if (ModelState.IsValid)
            {
                //Save image to wwwRoot/image
                string fileName = Path.GetFileNameWithoutExtension(application.ApplicationFile.FileName);
                string extension = Path.GetExtension(application.ApplicationFile.FileName);

                fileName += DateTime.Now.ToString("yymmssfff") + extension;
                application.FileName = fileName;

                string path = Path.Combine(_hostEnvironment.WebRootPath + "/image/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await application.ApplicationFile.CopyToAsync(fileStream);
                }

                application.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                //Insert record
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", application.UserId);
            return View(application);
        }

        // GET: Application/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Claims.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", application.UserId);
            return View(application);
        }

        // POST: Application/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Message,Theme,CurrentStatus,CreationTime,ModificationTime,FileName,UserId,ReplyId")] Application application)
        {
            if (id != application.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", application.UserId);
            return View(application);
        }

        // GET: Application/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Claims
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.Claims.FindAsync(id);

            //delete image from wwwroot/image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", application.FileName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _context.Claims.Remove(application);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return _context.Claims.Any(e => e.Id == id);
        }
    }
}
