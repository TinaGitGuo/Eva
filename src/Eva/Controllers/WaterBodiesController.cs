using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EVA.Data;
using EVA.Models;
using Eva;
//using Microsoft.AspNetCore.Hosting;

namespace EVA.Controllers
{
    public class WaterBodiesController : Controller
    {
        //private IHostingEnvironment _environment;

       

        private readonly EVAContext _context;

        public WaterBodiesController(EVAContext context) //, IHostingEnvironment environment)
        {
            _context = context;

            //_environment = environment;

        }

        // GET: WaterBodies
        public async Task<IActionResult> Index(string sortOrder, string currentFilter,string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;

            ViewData["SiteSortParm"] = String.IsNullOrEmpty(sortOrder) ? "site_desc" : "";

            ViewData["ConSortParm"] = sortOrder=="Const" ? "construction_desc" : "Const";

            ViewData["PTSortParm"] = sortOrder == "PT" ? "PT_desc" : "PT";

            ViewData["AreaSortParm"] = sortOrder == "Area" ? "Area_desc" : "Area";

            ViewData["VolSortParm"] = sortOrder == "Vol" ? "Vol_desc" : "Vol";


            if (searchString != null)
            {
                page = 1;
            }

            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var wb = from w in _context.WaterBodys
                     .Include(w => w.Construction)
                     .Include(w => w.Location)
                     .Include(w => w.PoolType)
                        //.ThenInclude(w=>w.iPoolType)
                     .Include(w => w.Site)
                     select w;

            //var sites = from s in _context.Sites
                          //select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                wb = wb.Where(s => s.Site.SiteName.Contains(searchString));
            }


            switch (sortOrder)
            {
                case "site_desc":
                    wb = wb.OrderByDescending(s => s.Site.SiteName);
                    break;

                case "construction_desc":
                    wb = wb.OrderByDescending(s => s.Construction.iConstruction);
                    break;

                case "Const":
                    wb = wb.OrderBy(s => s.Construction.iConstruction);
                    break;

               case "PT":
                    wb = wb.OrderBy(s => s.PoolType.iPoolType);
                   break;

              case "PT_desc":
                    wb = wb.OrderByDescending(s => s.PoolType.iPoolType);
                   break;

                case "Area":
                    wb = wb.OrderBy(s => s.PoolArea);
                    break;

                case "Area_desc":
                    wb = wb.OrderByDescending(s => s.PoolArea);
                    break;

                case "Vol":
                    wb = wb.OrderBy(s => s.PoolVolume);
                    break;

                case "Vol_desc":
                    wb = wb.OrderByDescending(s => s.PoolVolume);
                    break;

                default:
                    wb = wb.OrderBy(s => s.Site.SiteName);
                    break;

            }

           
            int pageSize = 7;

            return View(await PaginatedList<WaterBody>.CreateAsync(wb.AsNoTracking(), page ?? 1, pageSize));

           
        }

        // GET: WaterBodies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waterBody = await _context.WaterBodys
                .Include(w => w.Construction)
                .Include(w => w.Location)
                .Include(w => w.PoolType)
                   .ThenInclude(w=>w.HealthCategory)                                
                .Include(w => w.Site)
                .Include(w=>w.Equipment)
                    .ThenInclude(w=>w.Make)
                .Include(w => w.Equipment)
                    .ThenInclude(w=>w.Eqtype)                                
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.WaterBodyID == id);

           

            if (waterBody == null)
            {
                return NotFound();
            }

            return View(waterBody);
        }

        // GET: WaterBodies/Create
        public IActionResult Create()
        {
            ViewData["ConstructionID"] = new SelectList(_context.Constructions.OrderBy(c=>c.iConstruction), "ConstructionID", "iConstruction");
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "iLocation");
            ViewData["PoolTypeID"] = new SelectList(_context.PoolTypes.OrderBy(o=>o.iPoolType), "PoolTypeID", "iPoolType");
            ViewData["SiteID"] = new SelectList(_context.Sites.OrderBy(s=>s.SiteName), "SiteID", "SiteName");
            return View();
        }

        // POST: WaterBodies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create([Bind("SiteID,WBName,PoolTypeID,LocationID,ConstructionID,Length,Width,Depth")] WaterBody waterBody)
        {
            try
            {


                if (ModelState.IsValid)
                {
                   

                    _context.Add(waterBody);
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

            ViewData["ConstructionID"] = new SelectList(_context.Constructions, "ConstructionID", "ConstructionID", waterBody.ConstructionID);
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "LocationID", waterBody.LocationID);
            ViewData["PoolTypeID"] = new SelectList(_context.PoolTypes, "PoolTypeID", "PoolTypeID", waterBody.PoolTypeID);
            ViewData["SiteID"] = new SelectList(_context.Sites, "SiteID", "SiteAddress", waterBody.SiteID);
            return View(waterBody);
        }

        // GET: WaterBodies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waterBody = await _context.WaterBodys.SingleOrDefaultAsync(m => m.WaterBodyID == id);
            if (waterBody == null)
            {
                return NotFound();
            }
            ViewData["ConstructionID"] = new SelectList(_context.Constructions, "ConstructionID", "iConstruction", waterBody.ConstructionID);
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "iLocation", waterBody.LocationID);
            ViewData["PoolTypeID"] = new SelectList(_context.PoolTypes, "PoolTypeID", "iPoolType", waterBody.PoolTypeID);
            ViewData["SiteID"] = new SelectList(_context.Sites, "SiteID", "SiteName", waterBody.SiteID);
            return View(waterBody);
        }

        // POST: WaterBodies/Edit/5
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

            var wbToUpdate = await _context.WaterBodys.SingleOrDefaultAsync(w => w.WaterBodyID == id);

            if (await TryUpdateModelAsync<WaterBody>(
                wbToUpdate,
                "",
                w => w.WBName, w => w.SiteID, w => w.ConstructionID, w => w.Depth,
                w => w.Length, w => w.LocationID, w => w.PoolTypeID))
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
                        "Try again.  If error recurs see system administrator.");
                }

            }
            return View(wbToUpdate);
        }



        // GET: WaterBodies/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waterBody = await _context.WaterBodys.Include(w => w.Construction).Include(w => w.Location)
                .Include(w => w.PoolType).Include(w => w.Site)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.WaterBodyID == id);

            if (waterBody == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Deletion Failed.  Try again.  " +
                    "If error recurs see system administrator.";
            }


            return View(waterBody);
        }

        // POST: WaterBodies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var waterBody = await _context.WaterBodys
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.WaterBodyID == id);

            if (waterBody == null)
            {
                return RedirectToAction("Index");
            }

            try
            {

                _context.WaterBodys.Remove(waterBody);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            catch (DbUpdateException /*ex*/)
            {
                //log error uncomment ex to write log
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }

        }

        private bool WaterBodyExists(int id)
        {
            return _context.WaterBodys.Any(e => e.WaterBodyID == id);
        }
    }
}
