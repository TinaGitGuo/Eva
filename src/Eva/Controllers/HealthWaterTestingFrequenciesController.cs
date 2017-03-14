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
    public class HealthWaterTestingFrequenciesController : Controller
    {
        private readonly EVAContext _context;

        public HealthWaterTestingFrequenciesController(EVAContext context)
        {
            _context = context;    
        }

        // GET: HealthWaterTestingFrequencies
        public async Task<IActionResult> Index()
        {
            var EVAContext = _context.HealthWaterTestingFrequencys.Include(h => h.HealthGroup);
            return View(await EVAContext.ToListAsync());
        }

        // GET: HealthWaterTestingFrequencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthWaterTestingFrequency = await _context.HealthWaterTestingFrequencys.SingleOrDefaultAsync(m => m.ID == id);
            if (healthWaterTestingFrequency == null)
            {
                return NotFound();
            }

            return View(healthWaterTestingFrequency);
        }

        // GET: HealthWaterTestingFrequencies/Create
        public IActionResult Create()
        {
            ViewData["GroupID"] = new SelectList(_context.HealthGroups, "GroupID", "GroupID");
            return View();
        }

        // POST: HealthWaterTestingFrequencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Frequency,GroupID")] HealthWaterTestingFrequency healthWaterTestingFrequency)
        {
            if (ModelState.IsValid)
            {
                _context.Add(healthWaterTestingFrequency);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["GroupID"] = new SelectList(_context.HealthGroups, "GroupID", "GroupID", healthWaterTestingFrequency.GroupID);
            return View(healthWaterTestingFrequency);
        }

        // GET: HealthWaterTestingFrequencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthWaterTestingFrequency = await _context.HealthWaterTestingFrequencys.SingleOrDefaultAsync(m => m.ID == id);
            if (healthWaterTestingFrequency == null)
            {
                return NotFound();
            }
            ViewData["GroupID"] = new SelectList(_context.HealthGroups, "GroupID", "GroupID", healthWaterTestingFrequency.GroupID);
            return View(healthWaterTestingFrequency);
        }

        // POST: HealthWaterTestingFrequencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Frequency,GroupID")] HealthWaterTestingFrequency healthWaterTestingFrequency)
        {
            if (id != healthWaterTestingFrequency.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(healthWaterTestingFrequency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HealthWaterTestingFrequencyExists(healthWaterTestingFrequency.ID))
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
            ViewData["GroupID"] = new SelectList(_context.HealthGroups, "GroupID", "GroupID", healthWaterTestingFrequency.GroupID);
            return View(healthWaterTestingFrequency);
        }

        // GET: HealthWaterTestingFrequencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthWaterTestingFrequency = await _context.HealthWaterTestingFrequencys.SingleOrDefaultAsync(m => m.ID == id);
            if (healthWaterTestingFrequency == null)
            {
                return NotFound();
            }

            return View(healthWaterTestingFrequency);
        }

        // POST: HealthWaterTestingFrequencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var healthWaterTestingFrequency = await _context.HealthWaterTestingFrequencys.SingleOrDefaultAsync(m => m.ID == id);
            _context.HealthWaterTestingFrequencys.Remove(healthWaterTestingFrequency);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool HealthWaterTestingFrequencyExists(int id)
        {
            return _context.HealthWaterTestingFrequencys.Any(e => e.ID == id);
        }
    }
}
