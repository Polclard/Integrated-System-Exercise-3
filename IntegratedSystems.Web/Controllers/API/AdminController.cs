using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Service.Implementation;
using IntegratedSystems.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IntegratedSystems.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IVaccineService _vaccineService;
        private readonly IPatientService _patientService;
        private readonly IVaccinationCenterService _vaccinationCenterService;

        public AdminController(IVaccineService vaccineService,
            IPatientService patientService,
            IVaccinationCenterService vaccinationCenterService)
        {
            _vaccineService = vaccineService;
            _patientService = patientService;
            _vaccinationCenterService = vaccinationCenterService;
        }

        #region Vaccines
        [HttpGet("[action]")]
        public List<Vaccine> GetAllVaccines()
        {
            return this._vaccineService.GetAllVaccines();
        }
        [HttpPost("[action]")]
        public Vaccine GetDetailsForVaccine(BaseEntity baseEntity)
        {
            return this._vaccineService.GetDetailsForVaccine(baseEntity.Id);
        }
        #endregion

        #region Patient
        [HttpGet("[action]")]
        public List<Patient> GetAllPatients()
        {
            return this._patientService.GetAllPatients();
        }
        [HttpPost("[action]")]
        public Patient GetDetailsForPatient(BaseEntity baseEntity)
        {
            return this._patientService.GetDetailsForPatient(baseEntity.Id);
        }
        [HttpPost("[action]")]
        public List<Vaccine> GetAllVaccinesForPatient(BaseEntity baseEntity)
        {
            var allVaccinesForPatient = this._vaccineService.GetAllVaccinesFromPatient(baseEntity.Id);
            foreach(var vaccine in allVaccinesForPatient)
            {
                vaccine.Center = this._vaccinationCenterService.GetDetailsForVaccinationCenter(vaccine.VaccinationCenter);
            }
            return allVaccinesForPatient;
        }
        #endregion

        #region Vaccination Center
        //[HttpPost("[action]")]
        //public List<VaccinationCenter> GetAllVaccinationCentersForPatient(BaseEntity baseEntity)
        //{
        //    return this._vaccineService.getAllVac(baseEntity.Id);
        //}
        #endregion
    }
}
