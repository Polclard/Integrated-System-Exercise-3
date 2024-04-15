using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Interface
{
    public interface IVaccinationCenterService
    {
        List<VaccinationCenter> GetAllVaccinationCenters();
        VaccinationCenter GetDetailsForVaccinationCenter(Guid? id);
        VaccinationCenter CreateNewVaccinationCenter(VaccinationCenter p);
        VaccinationCenter UpdateExistingVaccinationCenter(VaccinationCenter p);
        VaccinationCenter DeleteVaccinationCenter(Guid id);
    }
}
