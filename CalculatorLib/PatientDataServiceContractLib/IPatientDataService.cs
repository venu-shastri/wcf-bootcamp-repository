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
        [System.ServiceModel.Web.WebGet(UriTemplate ="patients",ResponseFormat =System.ServiceModel.Web.WebMessageFormat.Json)]
        IEnumerable<DataModelsLib.PatientInfo> GetAllPatients();
        
        [System.ServiceModel.OperationContract]
        [System.ServiceModel.Web.WebInvoke(Method ="POST",UriTemplate ="patients/add",RequestFormat =System.ServiceModel.Web.WebMessageFormat.Json, ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        string AddNewPatient(DataModelsLib.PatientInfo newPatient);

        [System.ServiceModel.OperationContract]
        //[System.ServiceModel.Web.WebInvoke(Method = "DELETE", UriTemplate = "patients/remove/{mrn}", ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        bool RemovePatient(string mrn);

        [System.ServiceModel.OperationContract]
        //[System.ServiceModel.Web.WebInvoke(Method ="PUT",UriTemplate ="patients/update",RequestFormat =System.ServiceModel.Web.WebMessageFormat.Json)]
        bool UpdatePatient(PatientInfo updatedState);
    }
}
