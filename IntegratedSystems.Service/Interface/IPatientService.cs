using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Interface
{
    public interface IPatientService
    {
        List<Patient> GetAllPatients();
        Patient GetDetailsForPatient(Guid? id);
        Patient CreateNewPatient(Patient p);
        Patient UpdateExistingPatient(Patient p);
        Patient DeletePatient(Guid id);
    }
}
