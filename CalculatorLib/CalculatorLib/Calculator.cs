using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLib
{
    public class Calculator:CalculatorServiceContractLib.ICalculate
    {
        public int Add(int x,int y)
        {
            Console.WriteLine($"Add Invoked operand value {x} and {y}");
            return x + y;
        }
        public int Sub(int x,int y)
        {
            return x - y;
        }
    }
}
