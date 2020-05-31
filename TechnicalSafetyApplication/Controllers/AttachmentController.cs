using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechnicalSafetyApplication.Models;

namespace TechnicalSafetyApplication.Controllers
{
    public class AttachmentController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly AppIdentityDbContext _context;

        public AttachmentController(AppIdentityDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;

            if (hostEnvironment == null)
                throw new ArgumentNullException("hostEnvironment");

            _hostEnvironment = hostEnvironment;
        }

        // GET: Attachment
        public async Task<IActionResult> Index()
        {
            return View(await _context.Attachments.ToListAsync());
        }

        // GET: Attachment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachment = await _context.Attachments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attachment == null)
            {
                return NotFound();
            }

            return View(attachment);
        }

        // GET: Attachment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Attachment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,FormFile")] Attachment attachment)
        {
            if (ModelState.IsValid)
            {
                //Save image to wwwRoot/image
                string fileName = Path.GetFileNameWithoutExtension(attachment.FormFile.FileName);
                string extension = Path.GetExtension(attachment.FormFile.FileName);

                fileName += DateTime.Now.ToString("yymmssfff") + extension;
                attachment.Name = fileName;

                string path = Path.Combine(_hostEnvironment.WebRootPath + "/image", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await attachment.FormFile.CopyToAsync(fileStream);
                }


                //Insert record
                _context.Add(attachment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(attachment);
        }

        // GET: Attachment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachment = await _context.Attachments.FindAsync(id);
            if (attachment == null)
            {
                return NotFound();
            }
            return View(attachment);
        }

        // POST: Attachment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Name")] Attachment attachment)
        {
            if (id != attachment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attachment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttachmentExists(attachment.Id))
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
            return View(attachment);
        }

        // GET: Attachment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachment = await _context.Attachments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attachment == null)
            {
                return NotFound();
            }

            return View(attachment);
        }

        // POST: Attachment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attachment = await _context.Attachments.FindAsync(id);

            //delete image from wwwroot/image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", attachment.Name);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            //delete the record
            _context.Attachments.Remove(attachment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttachmentExists(int id)
        {
            return _context.Attachments.Any(e => e.Id == id);
        }
    }
}
