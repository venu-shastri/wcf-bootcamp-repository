using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerStatusServiceContractLib
{
    [System.ServiceModel.ServiceContract]
    
    public interface IServerStatusService
    {
        [System.ServiceModel.OperationContract]
        //REST -style
        //MAP http-verb and url template
        [System.ServiceModel.Web.WebInvoke(
            Method ="GET",
            UriTemplate ="servertime",
            ResponseFormat =System.ServiceModel.Web.WebMessageFormat.Json)]
        string GetServerTimeStamp();

        [System.ServiceModel.OperationContract]
        //REST -style
        //MAP http-verb and url template
        [System.ServiceModel.Web.WebInvoke(
           Method = "GET",
            // {data} = segment variable
           UriTemplate = "echo/{data}",
           ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        string echo(string data);
    }
}
