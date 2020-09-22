using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorServiceContractLib
{
    [System.ServiceModel.ServiceContract]
    public interface ICalculate
    {
        [System.ServiceModel.OperationContract]
        int Add(int x, int y);
        int Sub(int x, int y);
    }
}
