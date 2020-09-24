using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerStatusServiceLib
{
    public class ServerStatusService:ServerStatusServiceContractLib.IServerStatusService
    {
        public string echo(string data)
        {
            return data.ToUpper();
        }

        public string GetServerTimeStamp()
        {
            return DateTime.Now.ToLongTimeString();
        }
    }
}
