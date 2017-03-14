using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EVA.Data;
using EVA.Models;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace EVA.Controllers
{
    public class JobsController : Controller
    {
        private readonly EVAContext _context;

        public JobsController(EVAContext context)
        {
            _context = context;
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            var EVAContext = _context.Job
                .Include(j => j.Site)
                    .ThenInclude(j => j.WaterBodys)
                       // .ThenInclude(j=>j.WBName)
                .AsNoTracking()
                .SingleOrDefaultAsync();
                        
                        
              

            return View(await EVAContext);
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Include(m=>m.Site)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.JobID == id);




            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            ViewData["SiteID"] = new SelectList(_context.Sites, "SiteID", "SiteName");
            ViewData["WaterBodyID"] = new SelectList(_context.WaterBodys, "WaterBodyID", "WBName");
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "iDepartment");
            
            return View();
        }

        public IActionResult GetWaterBody(int siteID)
        {
            var waterBody = new List<WaterBody>();
            waterBody = getWaterBodyFromDataBaseBySiteID(siteID);
            return Json(waterBody);

        }

        public List<WaterBody> getWaterBodyFromDataBaseBySiteID(int siteID)
        {

            return _context.WaterBodys.Where(c => c.SiteID == siteID).OrderBy(w => w.WBName).ToList();
            //return _context.WaterBodys.ToList();
        }

        public IActionResult GetJobNumber(int clientID, int siteID, int waterBodyID)
        {
            
            var jN = clientID + " - " + siteID + " - " + waterBodyID + " - " + DateTime.Now;

            return View(jN);
        } 

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("DepartmentID,InvoiceDate,JobDescription,JobNumber,OrderNumber,SiteID,WaterBodyID")] Job job)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _context.Add(job);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

            }
            catch (DbUpdateException /*ex*/)
            {
                //Log the error uncomment ex to write a log
                ModelState.AddModelError("", "Changes not saved.  " +
                    "Try again.  Its life Jim.  But not as we know it!.");
            }


            ViewData["SiteID"] = new SelectList(_context.Sites, "SiteID", "SiteAddress", job.SiteID);
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job.SingleOrDefaultAsync(m => m.JobID == id);
            if (job == null)
            {
                return NotFound();
            }
            ViewData["SiteID"] = new SelectList(_context.Sites, "SiteID", "SiteAddress", job.SiteID);
            return View(job);
        }

        // POST: Jobs/Edit/5
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

        var jobToUpdate = await _context.Job.SingleOrDefaultAsync(j => j.JobID == id);

            if (await TryUpdateModelAsync<Job>(
                jobToUpdate,
                "",
                j=>j.SiteID, j=>j.WaterBodyID, j=>j.OrderNumber, j=>j.DepartmentID, j => j.JobDescription))
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
                        "Try again.  Its life Jim.  But not as we know it!.");
                }
            }

           
            ViewData["SiteID"] = new SelectList(_context.Sites, "SiteID", "SiteAddress", jobToUpdate.SiteID);
            return View(jobToUpdate);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.JobID == id);

            if (job == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your local alien.";
            }


            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Job
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.JobID == id);

            if (job == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _context.Job.Remove(job);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }


        }

        private bool JobExists(int id)
        {
            return _context.Job.Any(e => e.JobID == id);
        }
    }
}
