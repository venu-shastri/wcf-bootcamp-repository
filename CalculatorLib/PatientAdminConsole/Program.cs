using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientAdminConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PatientDataServiceReference.PatientDataServiceClient _proxy =
                new PatientDataServiceReference.PatientDataServiceClient("BasicHttpBinding_IPatientDataService");
            PatientDataServiceReference.PatientInfo _patientObj = new PatientDataServiceReference.PatientInfo
            {
                MRN = "P001",
                Name = "Tom",
                ContactNumber = "12345",
                Address = new PatientDataServiceReference.Address
                {
                    City = "BLR",
                    PinCode = "560007",
                    State = "KA",
                    Street = "XYZ"
                }
            };
            _proxy.AddNewPatient(_patientObj);

          var _patietnts=  _proxy.GetAllPatients();
            foreach(var patient in _patietnts)
            {
                Console.WriteLine($"{patient.MRN} {patient.Name} {patient.Address.City}");
            }
            Console.ReadLine();
        }
    }
}
