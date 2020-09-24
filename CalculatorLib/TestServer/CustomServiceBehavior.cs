using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace TestServer
{
    public class CustomServiceBehavior : IServiceBehavior
    {
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            //throw new NotImplementedException();
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, 
            ServiceHostBase serviceHostBase)
        {
           foreach(ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach(var ep in dispatcher.Endpoints)
                {
                    ep.DispatchRuntime.MessageInspectors.Add(new CustomMessageInspector());
                }
              
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            //throw new NotImplementedException();
        }
    }
}
