using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dns.IpTrace.Core.Models;

namespace Dns.IpTrace.Core
{
    public interface IIpTracer
    {
        IpAddressModel Get(string ipAddress);
        List<IpAddressModel>? Get(List<string> ipAddresses);
    }
}
