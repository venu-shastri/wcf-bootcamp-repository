using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Use Generated Proxy Class
            CalculatorServiceReference.CalculateClient _proxy = 
                // Use  EndpointName  from configuration File
                new CalculatorServiceReference.CalculateClient("NetNamedPipeBinding_ICalculate");
            Console.WriteLine(_proxy.Add(10, 20));
            Console.ReadLine();
        }
    }
}
