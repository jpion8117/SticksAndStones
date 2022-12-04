using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SticksAndStones.Models.DAL;

namespace SticksAndStones.Areas.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "SiteAdmin")]
    public class TaglinesController : Controller
    {
        private readonly SiteDataContext _context;

        public TaglinesController(SiteDataContext context)
        {
            _context = context;
        }

        // GET: Admin/Taglines
        public async Task<IActionResult> Index()
        {
            return View(await _context.Taglines.ToListAsync());
        }

        // GET: Admin/Taglines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagline = await _context.Taglines
                .FirstOrDefaultAsync(m => m.TaglineId == id);
            if (tagline == null)
            {
                return NotFound();
            }

            return View(tagline);
        }

        // GET: Admin/Taglines/Create
        public IActionResult Create()
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            ViewBag.UserId = user.Id;

            return View();
        }

        // POST: Admin/Taglines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaglineId,Content,Authorized,SuggestedById,AuthorizedById")] Tagline tagline)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tagline);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tagline);
        }

        // GET: Admin/Taglines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagline = await _context.Taglines.FindAsync(id);
            if (tagline == null)
            {
                return NotFound();
            }
            return View(tagline);
        }

        // POST: Admin/Taglines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaglineId,Content,Authorized,SuggestedById,AuthorizedById")] Tagline tagline)
        {
            if (id != tagline.TaglineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tagline);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaglineExists(tagline.TaglineId))
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
            return View(tagline);
        }

        // GET: Admin/Taglines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagline = await _context.Taglines
                .FirstOrDefaultAsync(m => m.TaglineId == id);
            if (tagline == null)
            {
                return NotFound();
            }

            return View(tagline);
        }

        // POST: Admin/Taglines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tagline = await _context.Taglines.FindAsync(id);
            _context.Taglines.Remove(tagline);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaglineExists(int id)
        {
            return _context.Taglines.Any(e => e.TaglineId == id);
        }
    }
}
