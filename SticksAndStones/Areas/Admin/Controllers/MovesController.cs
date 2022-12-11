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
    public class MovesController : Controller
    {
        private readonly SiteDataContext _context;

        public MovesController(SiteDataContext context)
        {
            _context = context;
        }

        // GET: Admin/Moves
        public async Task<IActionResult> Index()
        {
            var siteDataContext = _context.Moves.Include(m => m.Character);
            return View(await siteDataContext.ToListAsync());
        }

        // GET: Admin/Moves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var move = await _context.Moves
                .Include(m => m.Character)
                .FirstOrDefaultAsync(m => m.MoveId == id);
            if (move == null)
            {
                return NotFound();
            }

            return View(move);
        }

        // GET: Admin/Moves/Create
        public IActionResult Create()
        {
            //ViewData["CharacterId"] = new SelectList(_context.Characters, "CharacterId", "CharacterId");
            ViewData["Characters"] = _context.Characters.ToList();
            ViewData["Effects"] = _context.Effects.ToList();
            return View();
        }

        // POST: Admin/Moves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoveId,Name,Flavortext,CharacterId")] Move move, [Bind("effects")] int[] effects)
        {
            if (ModelState.IsValid)
            {
                foreach (var effect in effects)
                {
                    var moveEffect = new MoveEffect
                    {
                        Move = move,
                        MoveId = move.MoveId,
                        EffectId = effect,
                        Effect = _context.Effects.First(e => e.EffectId == effect)
                    };
                    _context.MoveEffects.Add(moveEffect);
                }

                _context.Add(move);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CharacterId"] = new SelectList(_context.Characters, "CharacterId", "CharacterId", move.CharacterId);
            ViewData["Characters"] = _context.Characters.ToList();
            ViewData["Effects"] = _context.Effects.ToList();
            return View(move);
        }

        // GET: Admin/Moves/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var move = _context.Moves.Find(id);
            if (move == null)
            {
                return NotFound();
            }
            //ViewData["CharacterId"] = new SelectList(_context.Characters, "CharacterId", "CharacterId", move.CharacterId);
            ViewData["Characters"] = _context.Characters.ToList();
            ViewData["Effects"] = _context.Effects.ToList();
            return View(move);
        }

        // POST: Admin/Moves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MoveId,Name,Flavortext,CharacterId")] Move move, [Bind("effectIds")] int[] effectIds)
        {
            if (id != move.MoveId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var moveEffects = _context.MoveEffects.Where(me => me.MoveId == id).ToList();
                 
                    //check for effects that need to be added
                    foreach (int effectId in effectIds)
                    {
                        if (!move.ContainsEffect(effectId, moveEffects))
                        {
                            var moveEffect = new MoveEffect
                            {
                                Move = move,
                                MoveId = move.MoveId,
                                EffectId = effectId,
                                Effect = _context.Effects.First(e => e.EffectId == effectId)
                            };
                            _context.MoveEffects.Add(moveEffect);
                        }
                    }

                    //check for effects that need to be removed
                    foreach (var effect in moveEffects)
                    {
                        if (!effectIds.Contains(effect.EffectId))
                        {
                            var removeEffect = _context.MoveEffects.Find(move.MoveId, effect.EffectId);
                            _context.MoveEffects.Remove(removeEffect);
                        }
                    }

                    _context.Update(move);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoveExists(move.MoveId))
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
            //ViewData["CharacterId"] = new SelectList(_context.Characters, "CharacterId", "CharacterId", move.CharacterId);
            ViewData["Characters"] = _context.Characters.ToList();
            ViewData["Effects"] = _context.Effects.ToList();
            return View(move);
        }

        // GET: Admin/Moves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var move = await _context.Moves
                .Include(m => m.Character)
                .FirstOrDefaultAsync(m => m.MoveId == id);
            if (move == null)
            {
                return NotFound();
            }

            return View(move);
        }

        // POST: Admin/Moves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var move = await _context.Moves.FindAsync(id);
            _context.Moves.Remove(move);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoveExists(int id)
        {
            return _context.Moves.Any(e => e.MoveId == id);
        }
    }
}
