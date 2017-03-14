using System;
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
    public class SitesController : Controller
    {
        private readonly EVAContext _context;

        public SitesController(EVAContext context)
        {
            _context = context;
        }

        // GET: Sites
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewData["CurrentSort"] = sortOrder;

            ViewData["SiteSortParm"] = String.IsNullOrEmpty(sortOrder) ? "site_desc" : "";

            ViewData["SubSortParm"] = sortOrder == "Sub" ? "sub_desc" : "Sub";

            ViewData["FactSortParm"] = sortOrder == "Fact" ? "fact_desc" : "Fact";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var site = from s in _context.Sites
                        .Include(s => s.SiteType)
                       select s;

            ViewData["CurrentFilter"] = searchString;

            

            if (!String.IsNullOrEmpty(searchString))
            {
                site = site.Where(s => s.SiteName.Contains(searchString));
            }


            switch (sortOrder)
            {
                case "site_desc":
                   site = site.OrderByDescending(s => s.SiteName);
                    break;

                
                case "Sub":
                    site = site.OrderBy(s => s.SiteSuburb);
                    break;

                case "sub_desc":
                    site =site.OrderByDescending(s => s.SiteSuburb);
                    break;

                case "Fact":
                    site = site.OrderBy(s => s.SiteType.SType);
                    break;

                case "fact_desc":
                    site = site.OrderByDescending(s => s.SiteType.SType);
                    break;

                default:
                    site = site.OrderBy(s => s.SiteName);
                    break;

            }

            int pageSize = 7;

            return View(await PaginatedList<Site>.CreateAsync(site.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Sites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = await _context.Sites
                .Include(w=>w.Client)
                .Include(w => w.SiteType)
                    .ThenInclude(w => w.HealthGroup)
                        .ThenInclude(w=>w.HealthGroupToAid)
                            .ThenInclude(w=>w.HealthFirstAid)  
               .Include(w=>w.SiteType)
                    .ThenInclude(w => w.HealthGroup)
                        .ThenInclude(w=>w.HEalthGroupToKit)                  
                            .ThenInclude(w=>w.HealthFirstAidKit)
               .Include(w => w.SiteType)
                    .ThenInclude(w => w.HealthGroup)
                        .ThenInclude(w=>w.HealthToilet)
               .Include(w => w.WaterBodys)
                        .ThenInclude(w => w.Equipment)
                    .AsNoTracking()
                .SingleOrDefaultAsync(m => m.SiteID == id);

            if (site == null)
            {
                return NotFound();
            }

            return View(site);

        }

        
        // GET: Sites/Create
        public IActionResult Create()
        {
            ViewData["SiteTypeID"] = new SelectList(_context.SiteTypes.OrderBy(s=>s.SType), "SiteTypeID", "SType");
            ViewData["ClientID"] = new SelectList(_context.Clients.OrderBy(c => c.Name), "ClientID", "Name");

            return View();
        }

        // POST: Sites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Clientid,SiteAddress,SiteName,SiteTypeID,SitePostCode,SiteSuburb")] Site site)
        {

            try
            {


                if (ModelState.IsValid)
                {
                    _context.Add(site);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

            }

            catch (DbUpdateException /*ex*/)
            {
                //Log the error uncomment ex to write a log
                ModelState.AddModelError("", "Changes not saved.  " +
                    "Try again.  If error recurs see system administrator.");
            }

            return View(site);
        }

        // GET: Sites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = await _context.Sites.SingleOrDefaultAsync(m => m.SiteID == id);
            if (site == null)
            {
                return NotFound();
            }
            return View(site);
        }

        // POST: Sites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id ==null)
            {
                return NotFound();
            }

            var siteToUpdate = await _context.Sites.SingleOrDefaultAsync(s => s.SiteID == id);

            if(await TryUpdateModelAsync<Site>(siteToUpdate,
                "",
                s=>s.SiteName, s=>s.SiteAddress, s=>s.SiteSuburb, s => s.SitePostCode))
            {
                try
                {
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            
                catch(DbUpdateException /*ex*/)
                    {
                    //log error uncomment ex to write log

                    ModelState.AddModelError("", "Changes not saved.  " +
                   "Try again.  If error recurs see system administrator.");
                }
            }
            return View(siteToUpdate);
        }

        // GET: Sites/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangedError=false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = await _context.Sites
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.SiteID == id);

            if (site == null)
            {
                return NotFound();
            }

            if (saveChangedError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Deletion Failed.  Try again.  " +
                    "If error recurs see system administrator.";
            }

            return View(site);
        }

        // POST: Sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var site = await _context.Sites
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.SiteID == id);

            if (site == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
          
                _context.Sites.Remove(site);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }

            catch (DbUpdateException /*ex*/)
            {
                //log error uncomment ex to write log
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }

        private bool SiteExists(int id)
        {
            return _context.Sites.Any(e => e.SiteID == id);
        }
    }
}
