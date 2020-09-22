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

            _wcfServiceHost.Open();// ServiceHost availability for Client Requests
            
            Console.ReadKey();
        }
    }
}
