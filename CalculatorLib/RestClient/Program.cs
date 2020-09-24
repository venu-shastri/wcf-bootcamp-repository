using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Http request
            System.Net.WebClient _httpRequest = new System.Net.WebClient();
            //string resultFromService=_httpRequest.DownloadString("http://localhost:8007/restservice/servertime");
            // Console.WriteLine(resultFromService);

            //post Method
            var _patient = new DataModelsLib.PatientInfo
            {
                MRN = "MRN009",
                Name = "Jacks",
                ContactNumber = "12345",
                Address = new DataModelsLib.Address { City = "Blr", PinCode = "56007", State = "Ka", Street = "abc" }
            };


            System.Net.HttpWebRequest _httpPostReq =
              System.Net.WebRequest.CreateHttp("http://localhost:8008/pic/hmgmt/patients/add");
            _httpPostReq.Method = "POST";
            _httpPostReq.ContentType = "application/json";
            System.Runtime.Serialization.Json.DataContractJsonSerializer patientDataJsonSerializer =
                new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(DataModelsLib.PatientInfo));
            patientDataJsonSerializer.WriteObject(_httpPostReq.GetRequestStream(), _patient);
            System.Net.HttpWebResponse response  = _httpPostReq.GetResponse() as HttpWebResponse;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("New Patient Added successfully");
            }

            //Get

            System.Net.HttpWebRequest _httpReq=
                System.Net.WebRequest.CreateHttp("http://localhost:8008/pic/hmgmt/patients");
            _httpReq.Method = "GET";
           response= _httpReq.GetResponse() as System.Net.HttpWebResponse;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Communication Successful");
                Console.WriteLine(response.ContentType);
                Console.WriteLine(response.ContentLength);
                System.Runtime.Serialization.Json.DataContractJsonSerializer _jsonSerializer =
                    new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(DataModelsLib.PatientInfo[]));
              DataModelsLib.PatientInfo[] patients= 
                    _jsonSerializer.ReadObject(response.GetResponseStream()) as DataModelsLib.PatientInfo[];
                foreach(var patient in patients)
                {
                    Console.WriteLine($"{patient.MRN} nad {patient.Name}");
                }
                
            }

           







        }
    }
}
