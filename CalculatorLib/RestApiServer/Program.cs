using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcf = System.ServiceModel;
using rest = System.ServiceModel.Web;
using System.IO;
using System.ServiceModel.Description;

namespace RestApiServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Code
            wcf.ServiceHost _serverStatusServiceHost =
                new wcf.ServiceHost(typeof(ServerStatusServiceLib.ServerStatusService));
            //rest client (web client) endpoint

           var endpoint= _serverStatusServiceHost.AddServiceEndpoint(
                typeof(ServerStatusServiceContractLib.IServerStatusService),
                new wcf.WebHttpBinding(),
                "http://localhost:8007/restservice");

            //add endpointbehavior to process incoming http request as a rest api request
            //enable web programming model

            endpoint.EndpointBehaviors.Add(new WebHttpBehavior());

            _serverStatusServiceHost.Open();
            Console.WriteLine("Restserver Started");
            Console.ReadKey();



        }
    }
}
