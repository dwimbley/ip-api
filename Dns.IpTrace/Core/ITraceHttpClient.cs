using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns.IpTrace.Core
{
    public interface ITraceHttpClient
    {
        string Send(string url);
        string Send(string url, string requestBody);
        string Send(string url, string requestBody, string contentType);

        Task<string> SendAsync(string url);
        Task<string> SendAsync(string url, string requestBody);
        Task<string> SendAsync(string url, string requestBody, string contentType);

    }
}
