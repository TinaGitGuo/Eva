using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EVA.Data;
using EVA.Models;

namespace EVA.Controllers
{
    public class HealthToiletsController : Controller
    {
        private readonly EVAContext _context;

        public HealthToiletsController(EVAContext context)
        {
            _context = context;    
        }

        // GET: HealthToilets
        public async Task<IActionResult> Index()
        {
            var EVAContext = _context.HealthToilets.Include(h => h.HealthGroup);
            return View(await EVAContext.ToListAsync());
        }

        // GET: HealthToilets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthToilet = await _context.HealthToilets.SingleOrDefaultAsync(m => m.HealthToiletID == id);
            if (healthToilet == null)
            {
                return NotFound();
            }

            return View(healthToilet);
        }

        // GET: HealthToilets/Create
        public IActionResult Create()
        {
            ViewData["GroupID"] = new SelectList(_context.HealthGroups, "GroupID", "GroupID");
            return View();
        }

        // POST: HealthToilets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HealthToiletID,FemaleWaterCloset,GroupID,Handbasins,LoadingFactor,MaleUrinals,MaleWaterCloset,ShowersPerPatron")] HealthToilet healthToilet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(healthToilet);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["GroupID"] = new SelectList(_context.HealthGroups, "GroupID", "GroupID", healthToilet.GroupID);
            return View(healthToilet);
        }

        // GET: HealthToilets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthToilet = await _context.HealthToilets.SingleOrDefaultAsync(m => m.HealthToiletID == id);
            if (healthToilet == null)
            {
                return NotFound();
            }
            ViewData["GroupID"] = new SelectList(_context.HealthGroups, "GroupID", "GroupID", healthToilet.GroupID);
            return View(healthToilet);
        }

        // POST: HealthToilets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HealthToiletID,FemaleWaterCloset,GroupID,Handbasins,LoadingFactor,MaleUrinals,MaleWaterCloset,ShowersPerPatron")] HealthToilet healthToilet)
        {
            if (id != healthToilet.HealthToiletID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(healthToilet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HealthToiletExists(healthToilet.HealthToiletID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["GroupID"] = new SelectList(_context.HealthGroups, "GroupID", "GroupID", healthToilet.GroupID);
            return View(healthToilet);
        }

        // GET: HealthToilets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthToilet = await _context.HealthToilets.SingleOrDefaultAsync(m => m.HealthToiletID == id);
            if (healthToilet == null)
            {
                return NotFound();
            }

            return View(healthToilet);
        }

        // POST: HealthToilets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var healthToilet = await _context.HealthToilets.SingleOrDefaultAsync(m => m.HealthToiletID == id);
            _context.HealthToilets.Remove(healthToilet);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool HealthToiletExists(int id)
        {
            return _context.HealthToilets.Any(e => e.HealthToiletID == id);
        }
    }
}
