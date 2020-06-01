using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechnicalSafetyApplication.Models;
using TechnicalSafetyApplication.Models.ViewModels;

namespace TechnicalSafetyApplication.Controllers
{
    public class ReplyController : Controller
    {
        private readonly AppIdentityDbContext _context;

        private Application _application;

        public ReplyController(AppIdentityDbContext context)
        {
            _context = context;
        }


        // GET: Reply
        public async Task<IActionResult> Request(int id)
        {
            var application = _context.Claims.Single(x => x.Id == id);
            application.CurrentStatus = Status.OnConsideration;
            var request = new Reply();

            var appRequest = new ApplicationRequestViewModel
            {
                Application = application,
                Request = request
            };

            _application = application;

            return View(appRequest);
        }

        // GET: Reply/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reply = await _context.Replies
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reply == null)
            {
                return NotFound();
            }

            return View(reply);
        }

        // GET: Reply/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Reply/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Request(ApplicationRequestViewModel applicationRequest)
        {
            if (ModelState.IsValid && applicationRequest.Application != null)
            {
                applicationRequest.Request.UserId = applicationRequest.Application.UserId;
                applicationRequest.Application.CurrentStatus = Status.Processed;

                _context.Add(applicationRequest.Request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", applicationRequest.Request.UserId);
            return View(applicationRequest.Request);
        }

        // GET: Reply/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reply = await _context.Replies.FindAsync(id);
            if (reply == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", reply.UserId);
            return View(reply);
        }

        // POST: Reply/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Message,CreationTime,ModificationTime,UserId")] Reply reply)
        {
            if (id != reply.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReplyExists(reply.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", reply.UserId);
            return View(reply);
        }

        private bool ReplyExists(int id)
        {
            return _context.Replies.Any(e => e.Id == id);
        }
    }
}
