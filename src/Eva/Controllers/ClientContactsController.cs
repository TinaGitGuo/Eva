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
    public class ClientContactsController : Controller
    {
        private readonly EVAContext _context;

        public ClientContactsController(EVAContext context)
        {
            _context = context;    
        }

        // GET: ClientContacts
        public async Task<IActionResult> Index()
        {
            var EVAContext = _context.ClientContacts
                .Include(c => c.Client)
                .Include(c => c.Position)
                .Include(c=>c.ClientContactDetail);

            return View(await EVAContext.ToListAsync());
        }

        // GET: ClientContacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientContact = await _context.ClientContacts
                .Include(c => c.ClientContactDetail)
                    .ThenInclude(c=>c.ContactType)
                       //.ThenInclude(c=>c.iContactType)                
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ClientContactID == id);

            if (clientContact == null)
            {
                return NotFound();
            }

            return View(clientContact);
        }

        // GET: ClientContacts/Create
        public IActionResult Create()
        {
            var clientContact =  _context.ClientContacts
               .Include(c => c.ClientContactDetail)
                  // .ThenInclude(c=>c.ContactType)
               //.Include(c => c.ClientContactDetail)
                 //  .ThenInclude(c => c.ContactDetail)
               .AsNoTracking()
               .SingleOrDefaultAsync();

            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "Name");
            ViewData["PositionID"] = new SelectList(_context.Positions, "PositionID", "iPosition");
            ViewData["ContactTypeID"] = new SelectList(_context.ContactTypes, "ContactTypeID", "iContactType");
            return View();
        }

        // POST: ClientContacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientID,Name,PositionID")] ClientContact clientContact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientContact);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "Name", clientContact.ClientID);
            ViewData["PositionID"] = new SelectList(_context.Positions, "PositionID", "iPosition", clientContact.PositionID);
            return View(clientContact);
        }

        // GET: ClientContacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientContact = await _context.ClientContacts.SingleOrDefaultAsync(m => m.ClientContactID == id);
            if (clientContact == null)
            {
                return NotFound();
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "Name", clientContact.ClientID);
            ViewData["PositionID"] = new SelectList(_context.Positions, "PositionID", "iPosition", clientContact.PositionID);
            return View(clientContact);
        }

        // POST: ClientContacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientContactID,ClientID,Name,PositionID")] ClientContact clientContact)
        {
            if (id != clientContact.ClientContactID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientContact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientContactExists(clientContact.ClientContactID))
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
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "Name", clientContact.ClientID);
            ViewData["PositionID"] = new SelectList(_context.Positions, "PositionID", "iPosition", clientContact.PositionID);
            return View(clientContact);
        }

        // GET: ClientContacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientContact = await _context.ClientContacts.SingleOrDefaultAsync(m => m.ClientContactID == id);
            if (clientContact == null)
            {
                return NotFound();
            }

            return View(clientContact);
        }

        // POST: ClientContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientContact = await _context.ClientContacts.SingleOrDefaultAsync(m => m.ClientContactID == id);
            _context.ClientContacts.Remove(clientContact);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ClientContactExists(int id)
        {
            return _context.ClientContacts.Any(e => e.ClientContactID == id);
        }
    }
}
