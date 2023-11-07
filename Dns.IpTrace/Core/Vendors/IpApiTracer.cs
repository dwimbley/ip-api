using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dns.IpTrace.Core.Models;
using Dns.IpTrace.Core.Vendors.Models;

namespace Dns.IpTrace.Core.Vendors
{
    public class IpApiTracer : IIpTracer
    {
        private readonly string _url = "http://ip-api.com/json/{ipaddress}";
        private readonly string _batchUrl = "http://ip-api.com/batch?fields=status,message,continent,continentCode,country,countryCode,region,regionName,city,district,zip,lat,lon,timezone,offset,currency,isp,org,as,asname,mobile,proxy,hosting,query";
        private readonly ITraceHttpClient _client;

        public IpApiTracer(ITraceHttpClient client) => this._client = client;

        public IpAddressModel Get(string ipAddress)
        {
            var url = this._url.Replace("{ipaddress}", ipAddress);
            var httpResponse = this._client.Send(url);
            var ipModel = JsonConvert.DeserializeObject<IpApiModel>(httpResponse);

            var model = new IpAddressModel
            {
                Status = ipModel.status,
                Country = ipModel.country,
                CountryCode = ipModel.countryCode,
                Region = ipModel.region,
                RegionName = ipModel.regionName,
                City = ipModel.city,
                Zip = ipModel.zip,
                Latitude = ipModel.lat,
                Longitude = ipModel.lon,
                TimeZone = ipModel.timezone,
                Isp = ipModel.isp,
                Org = ipModel.org,
                AutonomousSystemNumber = string.IsNullOrEmpty(ipModel.asname) ? ipModel.@as : ipModel.asname,
                Query = ipModel.query,
                IsMobile = ipModel.mobile,
                IsProxy = ipModel.proxy,
                IsHosting = ipModel.hosting
            };

            return model;
        }

        public List<IpAddressModel> Get(List<string> ipAddresses)
        {
            var jsonRequest = JsonConvert.SerializeObject(ipAddresses);
            var httpResponse = this._client.Send(_batchUrl, jsonRequest);
            var ipResults = JsonConvert.DeserializeObject<List<IpApiModel>>(httpResponse);

            // TODO: Need error handling if fails

            var mappedResults = new List<IpAddressModel>();

            foreach(var item in ipResults)
            {
                var ipmodel = new IpAddressModel
                {
                    Status = item.status,
                    Country = item.country,
                    CountryCode = item.countryCode,
                    Region = item.region,
                    RegionName = item.regionName,
                    City = item.city,
                    Zip = item.zip,
                    Latitude = item.lat,
                    Longitude = item.lon,
                    TimeZone = item.timezone,
                    Isp = item.isp,
                    Org = item.org,
                    AutonomousSystemNumber = String.IsNullOrEmpty(item.asname) ? item.@as : item.asname,
                    Query = item.query,
                    IsMobile = item.mobile,
                    IsProxy = item.proxy,
                    IsHosting = item.hosting
                };

                mappedResults.Add(ipmodel);
            }

            return mappedResults;
        }
    }
}
