using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Interface
{
    public interface IVaccineService
    {
        List<Vaccine> GetAllVaccines();
        Vaccine GetDetailsForVaccine(Guid? id);
        Vaccine CreateNewVaccine(Vaccine p);
        Vaccine UpdateExistingVaccine(Vaccine p);
        Vaccine DeleteVaccine(Guid id);
        List<Vaccine> GetAllVaccinesFromVaccinationCenter(Guid? id);
        List<Vaccine> GetAllVaccinesFromPatient(Guid? id);
    }
}
