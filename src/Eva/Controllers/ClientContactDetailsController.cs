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
    public class ClientContactDetailsController : Controller
    {
        private readonly EVAContext _context;

        public ClientContactDetailsController(EVAContext context)
        {
            _context = context;    
        }

        // GET: ClientContactDetails
        public async Task<IActionResult> Index()
        {
            var EVAContext = _context.ClientContactDetails.Include(c => c.ClientContact);
            return View(await EVAContext.ToListAsync());
        }

        // GET: ClientContactDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientContactDetail = await _context.ClientContactDetails.SingleOrDefaultAsync(m => m.ClientContactDetailID == id);
            if (clientContactDetail == null)
            {
                return NotFound();
            }

            return View(clientContactDetail);
        }

        // GET: ClientContactDetails/Create
        public IActionResult Create()
        {
            ViewData["ClientContactID"] = new SelectList(_context.ClientContacts, "ClientContactID", "Name");
            return View();
        }

        // POST: ClientContactDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientContactDetailID,ClientContactID,ContactDetail,ContactTypeID")] ClientContactDetail clientContactDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientContactDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ClientContactID"] = new SelectList(_context.ClientContacts, "ClientContactID", "Name", clientContactDetail.ClientContactID);
            return View(clientContactDetail);
        }

        // GET: ClientContactDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientContactDetail = await _context.ClientContactDetails.SingleOrDefaultAsync(m => m.ClientContactDetailID == id);
            if (clientContactDetail == null)
            {
                return NotFound();
            }
            ViewData["ClientContactID"] = new SelectList(_context.ClientContacts, "ClientContactID", "Name", clientContactDetail.ClientContactID);
            return View(clientContactDetail);
        }

        // POST: ClientContactDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientContactDetailID,ClientContactID,ContactDetail,ContactTypeID")] ClientContactDetail clientContactDetail)
        {
            if (id != clientContactDetail.ClientContactDetailID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientContactDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientContactDetailExists(clientContactDetail.ClientContactDetailID))
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
            ViewData["ClientContactID"] = new SelectList(_context.ClientContacts, "ClientContactID", "Name", clientContactDetail.ClientContactID);
            return View(clientContactDetail);
        }

        // GET: ClientContactDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientContactDetail = await _context.ClientContactDetails.SingleOrDefaultAsync(m => m.ClientContactDetailID == id);
            if (clientContactDetail == null)
            {
                return NotFound();
            }

            return View(clientContactDetail);
        }

        // POST: ClientContactDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientContactDetail = await _context.ClientContactDetails.SingleOrDefaultAsync(m => m.ClientContactDetailID == id);
            _context.ClientContactDetails.Remove(clientContactDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ClientContactDetailExists(int id)
        {
            return _context.ClientContactDetails.Any(e => e.ClientContactDetailID == id);
        }
    }
}
