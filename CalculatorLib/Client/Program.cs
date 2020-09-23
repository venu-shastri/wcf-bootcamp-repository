using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcf=System.ServiceModel;
namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //using namedpipe endpoint for the communication 

            wcf.EndpointAddress _address = new wcf.EndpointAddress("net.pipe://localhost/onmachinecchannel");
            wcf.NetNamedPipeBinding _pipeBinding = new wcf.NetNamedPipeBinding();

            //Building Proxy Object Using ChannelFactory class
          CalculatorServiceContractLib.ICalculate _proxy=
                wcf.ChannelFactory<CalculatorServiceContractLib.ICalculate>.CreateChannel
                (_pipeBinding, _address);

            int result = _proxy.Add(10, 20);
            Console.WriteLine(result);
            Console.ReadKey();
                

        }
    }
}
