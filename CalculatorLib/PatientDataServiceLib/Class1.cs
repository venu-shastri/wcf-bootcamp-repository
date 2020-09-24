using DataModelsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace PatientDataServiceLib
{
    [System.ServiceModel.ServiceBehavior(InstanceContextMode =System.ServiceModel.InstanceContextMode.Single)]
    public class PatientDataService : PatientDataServiceContractLib.IPatientDataService
    {
        List<DataModelsLib.PatientInfo> _db = new List<PatientInfo>();

        public PatientDataService()
        {
            Console.WriteLine("PatientDataService Instantiated");
            _db.Add(new PatientInfo
            {
                MRN = "MRN001",
                Name = "Tom",
                ContactNumber = "1234",
                Address = new Address { City = "Blr", PinCode = "56007", State = "Ka", Street = "abc" }
            });
            _db.Add(new PatientInfo
            {
                MRN = "MRN002",
                Name = "Hary",
                ContactNumber = "1234",
                Address = new Address { City = "Blr", PinCode = "56007", State = "Ka", Street = "abc" }
            });
            _db.Add(new PatientInfo
            {
                MRN = "MRN003",
                Name = "Jack",
                ContactNumber = "1234",
                Address = new Address { City = "Blr", PinCode = "56007", State = "Ka", Street = "abc" }
            });
        }
        public string AddNewPatient(PatientInfo newPatient)
        {
            Console.WriteLine("New Patient Details Recieved");
            _db.Add(newPatient);
            return newPatient.MRN;
        }

        //Enable Rest Api
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

        public bool UpdatePatient(PatientInfo updatedState)
        {
            return false;
        }
    }
}
