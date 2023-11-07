using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns.IpTrace.Core.Models
{
    public class IpAddressModel
    {
        public string? Status { get; set; }
        public string? Country { get; set; }
        public string? CountryCode { get; set; }
        public string? Region { get; set; }
        public string? RegionName { get; set; }
        public string? City { get; set; }
        public string? Zip { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? TimeZone { get; set; }
        public string? Isp { get; set; }
        public string? Org { get; set; }
        public string? AutonomousSystemNumber { get; set; }
        public string? Query { get; set; }
        public bool IsMobile { get; set; }
        public bool IsProxy { get; set; }
        public bool IsHosting { get; set; }
    }
}
