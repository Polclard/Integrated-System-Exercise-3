using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Implementation
{
    public class VaccineService : IVaccineService
    {
        private readonly IRepository<Vaccine> _vaccineRepository;

        public VaccineService(IRepository<Vaccine> vaccineRepository)
        {
            _vaccineRepository = vaccineRepository;
        }
        public Vaccine CreateNewVaccine(Vaccine p)
        {
            _vaccineRepository.Insert(p);
            return p;
        }

        public Vaccine DeleteVaccine(Guid id)
        {
            var patient = _vaccineRepository.Get(id);
            _vaccineRepository.Delete(patient);
            return patient;
        }

        public List<Vaccine> GetAllVaccines()
        {
            return _vaccineRepository.GetAll().ToList();
        }

        public List<Vaccine> GetAllVaccinesFromPatient(Guid? id)
        {
            return _vaccineRepository.GetAll().Where(vaccine => vaccine.PatientId == id).ToList();
        }

        public List<Vaccine> GetAllVaccinesFromVaccinationCenter(Guid? id)
        {
            return _vaccineRepository.GetAll().Where(vaccine => vaccine.VaccinationCenter == id).ToList();
        }

        public Vaccine GetDetailsForVaccine(Guid? id)
        {
            return _vaccineRepository.Get(id);
        }

        public Vaccine UpdateExistingVaccine(Vaccine p)
        {
            _vaccineRepository.Update(p);
            return p;
        }
    }
}
