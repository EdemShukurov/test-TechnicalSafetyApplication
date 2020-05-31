using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TechnicalSafetyApplication.Models;

namespace TechnicalSafetyApplication.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public ApplicationController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: Application
        public async Task<IActionResult> Index()
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userId = new SqlParameter("@userId", User.FindFirstValue(ClaimTypes.NameIdentifier));
            var list = _context.Claims.FromSqlRaw("EXECUTE GetApplicationsByUserId @userId", userId).ToList();

            var appIdentityDbContext = _context.Claims.Include(a => a.User);
            return View(await appIdentityDbContext.ToListAsync());
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
            ViewData["ReplyId"] = new SelectList(_context.Replies, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Application/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Message,Theme,CurrentStatus,CreationTime,ModificationTime,UserId,ReplyId")] Application application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReplyId"] = new SelectList(_context.Replies, "Id", "Id", application.ReplyId);
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
            ViewData["ReplyId"] = new SelectList(_context.Replies, "Id", "Id", application.ReplyId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", application.UserId);
            return View(application);
        }

        // POST: Application/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Message,Theme,CurrentStatus,CreationTime,ModificationTime,UserId,ReplyId")] Application application)
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
            ViewData["ReplyId"] = new SelectList(_context.Replies, "Id", "Id", application.ReplyId);
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
