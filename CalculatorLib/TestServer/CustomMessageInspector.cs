using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace TestServer
{
    public class CustomMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            System.IO.StreamWriter wr = new System.IO.StreamWriter("..//..//message.txt", true);
            wr.WriteLine(request.ToString());
            wr.WriteLine("**********************************************");
            wr.Flush();
            wr.Close();
            return System.DateTime.Now.ToString();
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            System.IO.StreamWriter wr = new System.IO.StreamWriter("..//..//message.txt", true);
            wr.WriteLine(reply.ToString());
            wr.WriteLine("**********************************************");
            wr.Flush();
            wr.Close();
        }
    }
}
