using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using F1World;
using F1World.Data;

namespace F1World.Controllers
{
    public class RaceResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RaceResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RaceResults
        public async Task<IActionResult> Index()
        {
            var ApplicationDbContext = _context.RaceResults.Include(r => r.Pilot).Include(r => r.Race);
            return View(await ApplicationDbContext.ToListAsync());
        }

        // GET: RaceResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raceResult = await _context.RaceResults
                .Include(r => r.Pilot)
                .Include(r => r.Race)
                .FirstOrDefaultAsync(m => m.RaceResultId == id);
            if (raceResult == null)
            {
                return NotFound();
            }

            return View(raceResult);
        }

        // GET: RaceResults/Create
        public IActionResult Create()
        {
            ViewData["PilotId"] = new SelectList(_context.Pilots, "PilotId", "PilotId");
            ViewData["RaceId"] = new SelectList(_context.Races, "RaceId", "RaceId");
            return View();
        }

        // POST: RaceResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RaceResultId,RaceId,PilotId,Position,FinishTime")] RaceResult raceResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(raceResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PilotId"] = new SelectList(_context.Pilots, "PilotId", "PilotId", raceResult.PilotId);
            ViewData["RaceId"] = new SelectList(_context.Races, "RaceId", "RaceId", raceResult.RaceId);
            return View(raceResult);
        }

        // GET: RaceResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raceResult = await _context.RaceResults.FindAsync(id);
            if (raceResult == null)
            {
                return NotFound();
            }
            ViewData["PilotId"] = new SelectList(_context.Pilots, "PilotId", "PilotId", raceResult.PilotId);
            ViewData["RaceId"] = new SelectList(_context.Races, "RaceId", "RaceId", raceResult.RaceId);
            return View(raceResult);
        }

        // POST: RaceResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RaceResultId,RaceId,PilotId,Position,FinishTime")] RaceResult raceResult)
        {
            if (id != raceResult.RaceResultId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(raceResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RaceResultExists(raceResult.RaceResultId))
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
            ViewData["PilotId"] = new SelectList(_context.Pilots, "PilotId", "PilotId", raceResult.PilotId);
            ViewData["RaceId"] = new SelectList(_context.Races, "RaceId", "RaceId", raceResult.RaceId);
            return View(raceResult);
        }

        // GET: RaceResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raceResult = await _context.RaceResults
                .Include(r => r.Pilot)
                .Include(r => r.Race)
                .FirstOrDefaultAsync(m => m.RaceResultId == id);
            if (raceResult == null)
            {
                return NotFound();
            }

            return View(raceResult);
        }

        // POST: RaceResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var raceResult = await _context.RaceResults.FindAsync(id);
            if (raceResult != null)
            {
                _context.RaceResults.Remove(raceResult);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RaceResultExists(int id)
        {
            return _context.RaceResults.Any(e => e.RaceResultId == id);
        }
    }
}
