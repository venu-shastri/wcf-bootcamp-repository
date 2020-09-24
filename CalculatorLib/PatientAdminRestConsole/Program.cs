using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientAdminRestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            System.ServiceModel.ServiceHost _host = new System.ServiceModel.ServiceHost(typeof(PatientDataServiceLib.PatientDataService));
            _host.Open();
            Console.WriteLine("Server Started");
            Console.ReadLine();
        }
    }
}
