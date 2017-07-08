using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;

namespace DBConnector.Tools
{
    public class WCFTracer : IClientMessageInspector, IEndpointBehavior
    {
        public event EventHandler OnMessageInspected;

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            if (OnMessageInspected != null)
            {
                OnMessageInspected(this, new MessageInspectorArgs { Message = reply.ToString(), MessageInspectionType = eMessageInspectionType.Response });
            }
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            if (OnMessageInspected != null)
            {
                OnMessageInspected(this, new MessageInspectorArgs { Message = request.ToString(), MessageInspectionType = eMessageInspectionType.Request });
            }
            return null;
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(this);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }

    public class MessageInspectorArgs : EventArgs
    {
        ///<summary>
        /// Type of the message inpected.
        /// </summary>
        public eMessageInspectionType MessageInspectionType { get; internal set; }

        ///<summary>
        /// Inspected raw message.
        /// </summary>
        public string Message { get; internal set; }
    }
}