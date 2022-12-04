using System;
using System.Collections.Generic;
using System.Linq;
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
    public class EffectsController : Controller
    {
        private readonly SiteDataContext _context;

        public EffectsController(SiteDataContext context)
        {
            _context = context;
        }

        // GET: Admin/Effects
        public async Task<IActionResult> Index()
        {
            return View(await _context.Effects.ToListAsync());
        }

        // GET: Admin/Effects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var effect = await _context.Effects
                .FirstOrDefaultAsync(m => m.EffectId == id);
            if (effect == null)
            {
                return NotFound();
            }

            return View(effect);
        }

        // GET: Admin/Effects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Effects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EffectId,Name,Flavortext")] Effect effect)
        {
            if (ModelState.IsValid)
            {
                _context.Add(effect);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(effect);
        }

        // GET: Admin/Effects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var effect = await _context.Effects.FindAsync(id);
            if (effect == null)
            {
                return NotFound();
            }
            return View(effect);
        }

        // POST: Admin/Effects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EffectId,Name,Flavortext")] Effect effect)
        {
            if (id != effect.EffectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(effect);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EffectExists(effect.EffectId))
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
            return View(effect);
        }

        // GET: Admin/Effects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var effect = await _context.Effects
                .FirstOrDefaultAsync(m => m.EffectId == id);
            if (effect == null)
            {
                return NotFound();
            }

            return View(effect);
        }

        // POST: Admin/Effects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var effect = await _context.Effects.FindAsync(id);
            _context.Effects.Remove(effect);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EffectExists(int id)
        {
            return _context.Effects.Any(e => e.EffectId == id);
        }
    }
}
