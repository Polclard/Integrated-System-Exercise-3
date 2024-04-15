using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository;
using IntegratedSystems.Service.Interface;
using IntegratedSystems.Domain.ENUM;

namespace IntegratedSystems.Web.Controllers
{
    public class VaccinesController : Controller
    {
        private readonly IVaccineService _vaccineService;
        private readonly IVaccinationCenterService _vaccinationCenterService;
        private readonly IPatientService _patientService;

        public VaccinesController(IVaccineService vaccineService,
            IVaccinationCenterService vaccinationCenterService,
            IPatientService patientService)
        {
            _vaccineService = vaccineService;
            _vaccinationCenterService = vaccinationCenterService;
            _patientService = patientService;
        }

        // GET: Vaccines
        public IActionResult Index()
        {
            //var applicationDbContext = _context.Vaccines.Include(v => v.PatientFor);
            return View(_vaccineService.GetAllVaccines().ToList());
        }

        // GET: Vaccines/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccine = _vaccineService.GetDetailsForVaccine(id);
            if (vaccine == null)
            {
                return NotFound();
            }

            return View(vaccine);
        }

        // GET: Vaccines/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_patientService.GetAllPatients(), "Id", "Embg");
            ViewData["VaccinationCenterId"] = new SelectList(_vaccinationCenterService.GetAllVaccinationCenters(), "Id", "Name");
            return View();
        }

        public IActionResult AddNewVacine(Guid? vacCenterId)
        {
            ViewData["PatientId"] = new SelectList(_patientService.GetAllPatients(), "Id", "FirstName");
            ViewData["VaccinationCenter"] = _vaccinationCenterService.GetDetailsForVaccinationCenter(vacCenterId);
            //ViewBag.ManufacturersEnum = Manufacturers.Pfizer;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewVacine([Bind("Manufacturer,DateTaken,PatientId,VaccinationCenter,Id")] Vaccine vaccine)
        {
            if (ModelState.IsValid)
            {
                vaccine.Id = Guid.NewGuid();
                vaccine.Certificate = Guid.NewGuid();
                _vaccineService.CreateNewVaccine(vaccine);
                return RedirectToAction("Index", "VaccinationCenters");
            }
            ViewData["PatientId"] = new SelectList(_patientService.GetAllPatients(), "Id", "Id", vaccine.PatientId);
            return View(vaccine);
        }

        // POST: Vaccines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Manufacturer,DateTaken,PatientId,VaccinationCenter,Id")] Vaccine vaccine)
        {
            if (ModelState.IsValid)
            {
                vaccine.Id = Guid.NewGuid();
                vaccine.Certificate = Guid.NewGuid();
                _vaccineService.CreateNewVaccine(vaccine);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_patientService.GetAllPatients(), "Id", "Id", vaccine.PatientId);
            return View(vaccine);
        }

        // GET: Vaccines/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccine = _vaccineService.GetDetailsForVaccine(id);
            if (vaccine == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_patientService.GetAllPatients(), "Id", "Id", vaccine.PatientId);
            return View(vaccine);
        }

        // POST: Vaccines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Manufacturer,Certificate,DateTaken,PatientId,VaccinationCenter,Id")] Vaccine vaccine)
        {
            if (id != vaccine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _vaccineService.UpdateExistingVaccine(vaccine);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccineExists(vaccine.Id))
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
            ViewData["PatientId"] = new SelectList(_patientService.GetAllPatients(), "Id", "Id", vaccine.PatientId);
            return View(vaccine);
        }

        // GET: Vaccines/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccine = _vaccineService.GetAllVaccines();
            if (vaccine == null)
            {
                return NotFound();
            }

            return View(vaccine);
        }

        // POST: Vaccines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            if (id != null)
            {
                _vaccineService.DeleteVaccine(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool VaccineExists(Guid id)
        {
            return _vaccineService.GetAllVaccines().Any(e => e.Id == id);
        }
    }
}
