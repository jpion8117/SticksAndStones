using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SticksAndStones.Models.DAL;

namespace SticksAndStones
{
    public class LeaderboardController : Controller
    {
        private readonly PlayerDataContext _context;

        public LeaderboardController(PlayerDataContext context)
        {
            _context = context;
        }

        // GET: Leaderboard
        public async Task<IActionResult> Index()
        {
            var playerDataContext = _context.Leaderboard.Include(l => l.User);
            return View(await playerDataContext.ToListAsync());
        }

        // GET: Leaderboard/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaderboard = await _context.Leaderboard
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (leaderboard == null)
            {
                return NotFound();
            }

            return View(leaderboard);
        }

        // GET: Leaderboard/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserName");
            return View();
        }

        // POST: Leaderboard/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Date,UserID,Ranking")] Leaderboard leaderboard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leaderboard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserName", leaderboard.UserID);
            return View(leaderboard);
        }

        // GET: Leaderboard/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaderboard = await _context.Leaderboard.FindAsync(id);
            if (leaderboard == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserName", leaderboard.UserID);
            return View(leaderboard);
        }

        // POST: Leaderboard/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Date,UserID,Ranking")] Leaderboard leaderboard)
        {
            if (id != leaderboard.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaderboard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaderboardExists(leaderboard.ID))
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
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserName", leaderboard.UserID);
            return View(leaderboard);
        }

        // GET: Leaderboard/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaderboard = await _context.Leaderboard
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (leaderboard == null)
            {
                return NotFound();
            }

            return View(leaderboard);
        }

        // POST: Leaderboard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaderboard = await _context.Leaderboard.FindAsync(id);
            _context.Leaderboard.Remove(leaderboard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaderboardExists(int id)
        {
            return _context.Leaderboard.Any(e => e.ID == id);
        }
    }
}
