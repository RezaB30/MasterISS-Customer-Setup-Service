using NLog;
using NLog.LayoutRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Web;

namespace RadiusR_Customer_Setup_Service
{
    [LayoutRenderer("wcf-ip-address")]
    public class IPLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            var context = OperationContext.Current;
            var messageProps = context.IncomingMessageProperties;
            var endPoint = messageProps[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            string ip = endPoint.Address;
            if (messageProps.Keys.Contains(HttpRequestMessageProperty.Name))
            {
                var endpointLoadBalancer = messageProps[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
                if (endpointLoadBalancer != null && endpointLoadBalancer.Headers["X-Forwarded-For"] != null)
                    ip = endpointLoadBalancer.Headers["X-Forwarded-For"];
                if (string.IsNullOrEmpty(ip))
                {
                    ip = endPoint.Address;
                }
            }

            builder.Append(ip);
        }
    }
}