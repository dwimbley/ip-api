using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dns.IpTrace.Core;
using Dns.IpTrace.Core.Vendors;

namespace Dns.IpTrace.Core
{
    public class IpTracerFactory : IIpTracerFactory
    {
        private readonly ITraceHttpClient _client;

        public IpTracerFactory(ITraceHttpClient client)
        {
            this._client = client;
        }

        public IIpTracer Get(string value = "IpApi")
        {
            switch (value)
            {
                case "IpApi":
                    return new IpApiTracer(this._client);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
