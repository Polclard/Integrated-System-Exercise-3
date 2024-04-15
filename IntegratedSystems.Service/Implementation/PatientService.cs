using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;

namespace IntegratedSystems.Service.Implementation
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<Patient> _patientRepository;

        public PatientService(IRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public Patient CreateNewPatient(Patient p)
        {
            _patientRepository.Insert(p);
            return p;
        }

        public Patient DeletePatient(Guid id)
        {
            var patient = _patientRepository.Get(id);
            _patientRepository.Delete(patient);
            return patient;
        }

        public List<Patient> GetAllPatients()
        {
            return _patientRepository.GetAll().ToList();
        }

        public Patient GetDetailsForPatient(Guid? id)
        {
            return _patientRepository.Get(id);
        }

        public Patient UpdateExistingPatient(Patient p)
        {
            _patientRepository.Update(p);
            return p;
        }
    }
}
