﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns.IpTrace.Core
{
    public interface IIpTracerFactory
    {
        IIpTracer Get(string value = "IpApi");
    }
}
