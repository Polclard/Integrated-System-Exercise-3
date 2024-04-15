using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;

namespace IntegratedSystems.Service.Implementation
{
    public class VaccinationCenterService : IVaccinationCenterService
    {
        private readonly IRepository<VaccinationCenter> _vaccinationCenterRepository;

        public VaccinationCenterService(IRepository<VaccinationCenter> vaccinationCenterRepository)
        {
            _vaccinationCenterRepository = vaccinationCenterRepository;
        }
        public VaccinationCenter CreateNewVaccinationCenter(VaccinationCenter p)
        {
            _vaccinationCenterRepository.Insert(p);
            return p;
        }

        public VaccinationCenter DeleteVaccinationCenter(Guid id)
        {
            var patient = _vaccinationCenterRepository.Get(id);
            _vaccinationCenterRepository.Delete(patient);
            return patient;
        }

        public List<VaccinationCenter> GetAllVaccinationCenters()
        {
            return _vaccinationCenterRepository.GetAll().ToList();
        }

        public VaccinationCenter GetDetailsForVaccinationCenter(Guid? id)
        {
            return _vaccinationCenterRepository.Get(id);
        }

        public VaccinationCenter UpdateExistingVaccinationCenter(VaccinationCenter p)
        {
            _vaccinationCenterRepository.Update(p);
            return p;
        }
    }
}
