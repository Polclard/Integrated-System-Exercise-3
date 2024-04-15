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

namespace IntegratedSystems.Web.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IVaccineService _vaccineService;
        private readonly IVaccinationCenterService _vaccinationCenterService;

        public PatientsController(IPatientService patientService,
            IVaccineService vaccineService,
            IVaccinationCenterService vaccinationCenterService)
        {
            _patientService = patientService;
            _vaccineService = vaccineService;
            _vaccinationCenterService = vaccinationCenterService;
        }

        // GET: Patients
        public IActionResult Index()
        {
            return View(_patientService.GetAllPatients().ToList());
        }

        // GET: Patients/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccines = _vaccineService.GetAllVaccinesFromPatient(id);
            for(int i = 0; i < vaccines.Count(); i++)
            {
                vaccines[i].Center = _vaccinationCenterService.GetDetailsForVaccinationCenter(vaccines[i].VaccinationCenter);
            }
            ViewData["Patient"] = _patientService.GetDetailsForPatient(id);
            if (vaccines == null)
            {
                return NotFound();
            }

            return View(vaccines);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Embg,FirstName,LastName,PhoneNumber,Email,Id")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                patient.Id = Guid.NewGuid();
                _patientService.CreateNewPatient(patient);
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _patientService.GetDetailsForPatient(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Embg,FirstName,LastName,PhoneNumber,Email,Id")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _patientService.UpdateExistingPatient(patient);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
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
            return View(patient);
        }

        // GET: Patients/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _patientService.GetDetailsForPatient(id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (id != null)
            {
               _patientService.DeletePatient(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(Guid id)
        {
            return _patientService.GetAllPatients().Any(e => e.Id == id);
        }
    }
}
