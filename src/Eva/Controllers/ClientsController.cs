using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EVA.Data;
using EVA.Models;
using Eva;

namespace EVA.Controllers
{
    public class ClientsController : Controller
    {
        private readonly EVAContext _context;

        public ClientsController(EVAContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["ClientSortParm"] = sortOrder;
            ViewData["AccSortParm"] = sortOrder == "Acc" ? "acc_desc" : "Acc";
            ViewData["SubSortParm"] = sortOrder == "Sub" ? "sub_desc" : "Sub";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {

                searchString = currentFilter;

            }

            ViewData["CurrentFilter"] = searchString;

            var clients = from c in _context.Clients
                          select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(n => n.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    clients = clients.OrderByDescending(c => c.Name);
                    break;

                case "Acc":
                    clients = clients.OrderBy(c => c.ShentonAcc);
                    break;

                case "acc_desc":
                    clients = clients.OrderByDescending(c => c.ShentonAcc);
                    break;

                case "Sub":
                    clients = clients.OrderBy(c => c.Suburb);
                    break;

                case "sub_desc":
                    clients = clients.OrderByDescending(c => c.Suburb);
                    break;

                default:
                    clients = clients.OrderBy(c => c.Name);
                    break;


            }

            int pageSize = 7;

            return View(await PaginatedList<Client>.CreateAsync(clients.AsNoTracking(), page??1,pageSize));
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Site)
                    .ThenInclude(c => c.WaterBodys)                        
                .AsNoTracking()
            .SingleOrDefaultAsync(m => m.ClientID == id);
                

           


            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("AccountContact,AccountEmail,Address,Name,OfficePhone,PostCode,ShentonAcc,Suburb")] Client client)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _context.Add(client);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            catch (DbUpdateException /*ex*/)
            {
                //Log the error uncomment ex to write a log
                ModelState.AddModelError("", "Changes not saved.  " +
                    "Try again.  If error recurs Beam Me Up Scotty");
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.SingleOrDefaultAsync(m => m.ClientID == id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)       
           
        {

            if (id == null)
            {
                return NotFound();
            }

            var clientToUpdate = await _context.Clients.SingleOrDefaultAsync(c => c.ClientID == id);

            if (await TryUpdateModelAsync<Client>(
                clientToUpdate,
                "",
                c=>c.AccountContact, c=>c.AccountEmail, c=>c.Address, c=>c.Name, c=>c.OfficePhone, c=>c.PostCode, c => c.ShentonAcc, c => c.Suburb))

            {
                try
                {
                    
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException /*ex*/)
                {
                    //Log the error uncomment ex to write a log
                    ModelState.AddModelError("", "Changes not saved.  " +
                        "Try again.  If error recurs Beam Me Up Scotty");
                }
               
            }
            return View(clientToUpdate);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ClientID == id);

            if (client == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "Beam Me Up Scotty.";
            }


            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          
            try
            {
                Client clientToDelete = new Client() { ClientID = id };
                _context.Entry(clientToDelete).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
 
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ClientID == id);
        }
    }
}
