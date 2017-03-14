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
    public class EquipmentsController : Controller
    {
        private readonly EVAContext _context;

        public EquipmentsController(EVAContext context)
        {
            _context = context;
        }

        // GET: Equipments
        public async Task<IActionResult> Index()
        {
            var EVAContext = _context.Equipments.Include(e => e.Condition).Include(e => e.Eqtype).Include(e => e.Make).Include(e => e.Waterbody);
            return View(await EVAContext.ToListAsync());
        }

        // GET: Equipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments
                .SingleOrDefaultAsync(m => m.EQDataID == id);

            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // GET: Equipments/Create
        public IActionResult Create()
        {
            ViewData["ConditionID"] = new SelectList(_context.Conditions.OrderBy(e => e.EqCondition), "ConditionID", "EqCondition");
            ViewData["EqTypeID"] = new SelectList(_context.EqTypes.OrderBy(e => e.EquipmentType), "EqTypeID", "EquipmentType");
            ViewData["MakeID"] = new SelectList(_context.Makes.OrderBy(e => e.EqMake), "MakeID", "EqMake");
            //ViewData["WaterBodyID"] = new SelectList(_context.WaterBodys.OrderBy(e => e.WBName), "WaterBodyID", "WBName");
            //ViewData["SiteID"] = new SelectList(_context.Sites.OrderBy(e => e.SiteName), "SiteID", "SiteName");
            ViewData["WaterBodyID"] = new SelectList(_context.Departments.ToList(), "DivisionID", "iDepartment" );
            ViewData["SiteID"] = new SelectList(_context.Departments.ToList(), "iDepartment", "DivisionID" );
            return View();
        }

        // POST: Equipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ConditionID,EqTypeID,EstimateRemainingLife,InstallDate,LastServiceDate,MakeID,Model,Notes,Recommended,ReplacementCost,SerialNumber,WaterBodyID")] Equipment equipment)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _context.Add(equipment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

            }

            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "Danger!! Will Robertson Danger!!!!!!!!!!!");
            }


            ViewData["ConditionID"] = new SelectList(_context.Conditions.OrderBy(e => e.EqCondition), "ConditionID", "EqCondition", equipment.ConditionID);
            ViewData["EqTypeID"] = new SelectList(_context.EqTypes.OrderBy(e => e.EquipmentType), "EqTypeID", "EquipmentType", equipment.EqTypeID);
            ViewData["MakeID"] = new SelectList(_context.Makes.OrderBy(e => e.EqMake), "MakeID", "EqMake", equipment.MakeID);
            ViewData["WaterBodyID"] = new SelectList(_context.WaterBodys.OrderBy(e => e.WBName), "WaterBodyID", "WBName", equipment.WaterBodyID);
            ViewData["SiteID"] = new SelectList(_context.Sites.OrderBy(e => e.SiteName), "SiteID", "SiteName", equipment.Waterbody.SiteID);
          
            return View(equipment);
        }

        public JsonResult GetWaterBody(int ID)
        {
            var waterBody = new List<Department>();


            waterBody = getWaterBodyFromDataBaseBySiteID(ID);



            return Json(waterBody);

        }

        public List<Department> getWaterBodyFromDataBaseBySiteID(int siteID)
        {
            return _context.Departments.ToList();
            //return _context.WaterBodys.Where(c => c.SiteID == siteID).OrderBy(w => w.WBName).ToList();
        }

        // GET: Equipments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.EQDataID == id);

            if (equipment == null)
            {
                return NotFound();
            }

            ViewData["ConditionID"] = new SelectList(_context.Conditions.OrderBy(e => e.EqCondition), "ConditionID", "EqCondition", equipment.ConditionID);
            ViewData["EqTypeID"] = new SelectList(_context.EqTypes.OrderBy(e => e.EquipmentType), "EqTypeID", "EquipmentType", equipment.EqTypeID);
            ViewData["MakeID"] = new SelectList(_context.Makes.OrderBy(e => e.EqMake), "MakeID", "EqMake", equipment.MakeID);
            ViewData["WaterBodyID"] = new SelectList(_context.WaterBodys.OrderBy(e => e.WBName), "WaterBodyID", "WBName", equipment.WaterBodyID);
            //ViewData["SiteID"] = new SelectList(_context.Sites.OrderBy(e => e.SiteName), "SiteID", "SiteName", equipment.Waterbody.SiteID);
            return View(equipment);
        }

        // POST: Equipments/Edit/5
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
            var equipmentToUpdate = await _context.Equipments.SingleOrDefaultAsync(s => s.EQDataID == id);
            if (await TryUpdateModelAsync<Equipment>(
                equipmentToUpdate,
                "",
                e => e.WaterBodyID, e => e.EqTypeID, e => e.MakeID, e => e.Model, e => e.SerialNumber, e => e.InstallDate,
                e => e.EstimateRemainingLife, e => e.LastServiceDate, e => e.Recommended, e => e.Notes,
                e => e.ConditionID, e => e.ReplacementCost))
            {


                try
                {

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists " +
                        "Danger!! Will Robertson Danger!!!!!!!!!!!");
                }
            }
            ViewData["ConditionID"] = new SelectList(_context.Conditions.OrderBy(e => e.EqCondition), "ConditionID", "EqCondition", equipmentToUpdate.ConditionID);
            ViewData["EqTypeID"] = new SelectList(_context.EqTypes.OrderBy(e => e.EquipmentType), "EqTypeID", "EquipmentType", equipmentToUpdate.EqTypeID);
            ViewData["MakeID"] = new SelectList(_context.Makes.OrderBy(e => e.EqMake), "MakeID", "EqMake", equipmentToUpdate.MakeID);
            ViewData["WaterBodyID"] = new SelectList(_context.WaterBodys.OrderBy(e => e.WBName), "WaterBodyID", "WBName", equipmentToUpdate.WaterBodyID);
            //ViewData["SiteID"] = new SelectList(_context.Sites.OrderBy(e => e.SiteName), "SiteID", "SiteName", equipmentToUpdate.Waterbody.SiteID);
            return View(equipmentToUpdate);
        }

        // GET: Equipments/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments
                .Include(e => e.Condition)
                .Include(e => e.Eqtype)
                .Include(e => e.Make)
                .Include(e => e.Waterbody)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.EQDataID == id);

            if (equipment == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "Lost In Space.";
            }



            return View(equipment);
        }

        // POST: Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipment = await _context.Equipments
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.EQDataID == id);

            if (equipment == null)
            {
                return RedirectToAction("Index");
            }
            try
            {

                _context.Equipments.Remove(equipment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }
    }
        }

        

