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
    public class StaffsController : Controller
    {
        private readonly EVAContext _context;

        public StaffsController(EVAContext context)
        {
            _context = context;    
        }

        // GET: Staffs
        public async Task<IActionResult> Index()
        {
            var EVAContext = _context.Staffs.Include(s => s.Division);
            return View(await EVAContext.ToListAsync());
        }

        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs.SingleOrDefaultAsync(m => m.StaffID == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: Staffs/Create
        public IActionResult Create()
        {
            ViewData["DivisionID"] = new SelectList(_context.Divisions, "DivisionID", "DivisionID");
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffID,DivisionID,HireDate,StaffName,TerminationDate")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["DivisionID"] = new SelectList(_context.Divisions, "DivisionID", "DivisionID", staff.DivisionID);
            return View(staff);
        }

        // GET: Staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs.SingleOrDefaultAsync(m => m.StaffID == id);
            if (staff == null)
            {
                return NotFound();
            }
            ViewData["DivisionID"] = new SelectList(_context.Divisions, "DivisionID", "DivisionID", staff.DivisionID);
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffID,DivisionID,HireDate,StaffName,TerminationDate")] Staff staff)
        {
            if (id != staff.StaffID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staff.StaffID))
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
            ViewData["DivisionID"] = new SelectList(_context.Divisions, "DivisionID", "DivisionID", staff.DivisionID);
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs.SingleOrDefaultAsync(m => m.StaffID == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staff = await _context.Staffs.SingleOrDefaultAsync(m => m.StaffID == id);
            _context.Staffs.Remove(staff);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StaffExists(int id)
        {
            return _context.Staffs.Any(e => e.StaffID == id);
        }
    }
}
