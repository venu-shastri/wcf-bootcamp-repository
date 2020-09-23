using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using wcf= System.ServiceModel;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server Started and Listening.....");

#region CalculatorService Registration
            //Host service or Service Instance
            wcf.ServiceHost _wcfServiceHost = new wcf.ServiceHost(typeof(CalculatorLib.Calculator));
            //Add Endpoint
            System.Type _contract = typeof(CalculatorServiceContractLib.ICalculate);
            //Bidning
            wcf.NetNamedPipeBinding _localMachineBidning = new wcf.NetNamedPipeBinding();
            //Address 
            string address = "net.pipe://localhost/onmachinecchannel";

            //wcf.Description.ServiceEndpoint _localMachineCommunicationChannel = 
            //    new wcf.Description.ServiceEndpoint(
            //    new wcf.Description.ContractDescription(_contract.FullName),
            //    _localMachineBidning, 
            //    new wcf.EndpointAddress(address)
            //    );
            //_wcfServiceHost.AddServiceEndpoint(_localMachineCommunicationChannel);
            _wcfServiceHost.AddServiceEndpoint(_contract, _localMachineBidning, address);

            //LAN Clients
            _wcfServiceHost.AddServiceEndpoint(_contract, new NetTcpBinding(), "net.tcp://localhost:5000/lanep");
            //WAN
            _wcfServiceHost.AddServiceEndpoint(_contract, new BasicHttpBinding(), "http://localhost:8001/wanep");

            //Service Behavior to publish metadata (WSDL Document)
            wcf.Description.ServiceMetadataBehavior _metadataBehavior = new wcf.Description.ServiceMetadataBehavior();
            //Configure Behavior to publish metadata when ServiceHost recieves http-get request
            _metadataBehavior.HttpGetEnabled = true;
            //Define URL to download metadata;
            _metadataBehavior.HttpGetUrl =new Uri( "http://localhost:8002/metadata"); // this address used only for metadata download 

            //Add Behavior -> ServieHost
            _wcfServiceHost.Description.Behaviors.Add(_metadataBehavior);

            _wcfServiceHost.Open();// ServiceHost availability for Client Requests
            #endregion
            #region PatientDataServiceRegistration


            wcf.ServiceHost _patientDataServiceHost = 
                /* References Behvaior  End Point Details From app.config file */
                new ServiceHost(typeof(PatientDataServiceLib.PatientDataService));
            _patientDataServiceHost.Open();
            #endregion

            Console.ReadKey();
        }
    }
}
