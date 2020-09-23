using DataModelsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace PatientDataServiceLib
{
    public class PatientDataService : PatientDataServiceContractLib.IPatientDataService
    {
        List<DataModelsLib.PatientInfo> _db = new List<PatientInfo>();
        public string AddNewPatient(PatientInfo newPatient)
        {
            Console.WriteLine("New Patient Details Recieved");
            _db.Add(newPatient);
            return newPatient.MRN;
        }

        public IEnumerable<PatientInfo> GetAllPatients()
        {
            Console.WriteLine("Get All Patients Recieved New Request");
            return _db;
        }

        public bool RemovePatient(string mrn)
        {
            //search
            return false;
        }

        public bool UpdatePatient(string mrn, PatientInfo updatedState)
        {
            return false;
        }
    }
}
