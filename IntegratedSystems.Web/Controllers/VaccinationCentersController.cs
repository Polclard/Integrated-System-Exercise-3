using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository;
using IntegratedSystems.Service.Interface;
using IntegratedSystems.Service.Implementation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace IntegratedSystems.Web.Controllers
{
    public class VaccinationCentersController : Controller
    {
        private readonly IVaccinationCenterService _vaccinationCenterService;
        private readonly IVaccineService _vaccineService;

        public VaccinationCentersController(IVaccinationCenterService vaccinationCenterService,
            IVaccineService vaccineService)
        {
            _vaccinationCenterService = vaccinationCenterService;
            _vaccineService = vaccineService;
        }

        // GET: VaccinationCenters
        public IActionResult Index()
        {
            return View(_vaccinationCenterService.GetAllVaccinationCenters().ToList());
        }

        // GET: VaccinationCenters/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var allVaccinesFromVaccinationCenter = _vaccineService.GetAllVaccinesFromVaccinationCenter(id);
            ViewData["VaccinationCenterName"] = _vaccinationCenterService.GetDetailsForVaccinationCenter(id);
            if (allVaccinesFromVaccinationCenter == null)
            {
                return NotFound();
            }

            return View(allVaccinesFromVaccinationCenter);
        }

        public IActionResult CheckRedirectToAddVaccine(Guid? id)
        {
            var allVaccinesFromVaccinationCenter = _vaccineService.GetAllVaccinesFromVaccinationCenter(id);
            var vaccinationCenter = _vaccinationCenterService.GetDetailsForVaccinationCenter(id);
            if(allVaccinesFromVaccinationCenter.Count() < vaccinationCenter.MaxCapacity)
            {
                return RedirectToAction("AddNewVacine", "Vaccines", new { vacCenterId = vaccinationCenter.Id});
            }
            else
            {
                return Content("Max Capacity Reached");
            }    
        }

        // GET: VaccinationCenters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VaccinationCenters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Address,MaxCapacity,Id")] VaccinationCenter vaccinationCenter)
        {
            if (ModelState.IsValid)
            {
                vaccinationCenter.Id = Guid.NewGuid();
                _vaccinationCenterService.CreateNewVaccinationCenter(vaccinationCenter);
                return RedirectToAction(nameof(Index));
            }
            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = _vaccinationCenterService.GetDetailsForVaccinationCenter(id);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }
            return View(vaccinationCenter);
        }

        // POST: VaccinationCenters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Address,MaxCapacity,Id")] VaccinationCenter vaccinationCenter)
        {
            if (id != vaccinationCenter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _vaccinationCenterService.UpdateExistingVaccinationCenter(vaccinationCenter);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccinationCenterExists(vaccinationCenter.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = _vaccinationCenterService.GetDetailsForVaccinationCenter(id);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }

            return View(vaccinationCenter);
        }

        // POST: VaccinationCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            if (id != null)
            {
                _vaccinationCenterService.DeleteVaccinationCenter(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool VaccinationCenterExists(Guid id)
        {
            return _vaccinationCenterService.GetAllVaccinationCenters().Any(e => e.Id == id);
        }
    }
}
