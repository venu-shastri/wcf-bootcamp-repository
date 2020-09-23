using DataModelsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDataServiceContractLib
{
    [System.ServiceModel.ServiceContract]
    public interface IPatientDataService
    {
        [System.ServiceModel.OperationContract]
        IEnumerable<DataModelsLib.PatientInfo> GetAllPatients();
        
        [System.ServiceModel.OperationContract]
        string AddNewPatient(DataModelsLib.PatientInfo newPatient);

        [System.ServiceModel.OperationContract]
        bool RemovePatient(string mrn);

        [System.ServiceModel.OperationContract]
        bool UpdatePatient(string mrn, PatientInfo updatedState);
    }
}
