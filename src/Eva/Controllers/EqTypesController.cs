using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EVA.Data;
using EVA.Models;

namespace EVA.Controllers
{
    public class EqTypesController : Controller
    {
        private readonly EVAContext _context;

        public EqTypesController(EVAContext context)
        {
            _context = context;
        }



        // GET: EqTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EqTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EQFunction,EquipmentType")] EqType eqType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eqType);
                await _context.SaveChangesAsync();
                return RedirectToAction("../Equipments/Create");
            }
            return View("../Equipments/Create");
        }

    }
     
}
