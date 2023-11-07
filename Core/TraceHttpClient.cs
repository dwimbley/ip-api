using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns.IpTrace.Core
{
    public class TraceHttpClient : ITraceHttpClient
    {
        public string Send(string url)
        {
            var httpResponse = this.SendHttpRequest(url, string.Empty, string.Empty);
            return httpResponse;
        }

        public string Send(string url, string requestBody)
        {
            var httpResponse = this.SendHttpRequest(url, requestBody, "application/json");
            return httpResponse;
        }

        public string Send(string url, string requestBody, string contentType)
        {
            var httpResponse = this.SendHttpRequest(url, requestBody, contentType);
            return httpResponse;
        }

        public async Task<string> SendAsync(string url)
        {
            var httpResponse = await this.SendHttpRequestAsync(url, string.Empty, string.Empty);
            return httpResponse;
        }

        public async Task<string> SendAsync(string url, string requestBody)
        {
            var httpResponse = await this.SendHttpRequestAsync(url, requestBody, "application/json");
            return httpResponse;
        }

        public async Task<string> SendAsync(string url, string requestBody, string contentType)
        {
            var httpResponse = await this.SendHttpRequestAsync(url, requestBody, contentType);
            return httpResponse;
        }

        private async Task<string> SendHttpRequestAsync(string url, string requestBody, string contentType)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(url);

            HttpResponseMessage response;
            if (!string.IsNullOrEmpty(requestBody))
            {
                var content = new StringContent(requestBody, Encoding.UTF8, contentType);
                var httpMsg = new HttpRequestMessage(HttpMethod.Post, url);
                httpMsg.Content = content;
                response = await httpClient.SendAsync(httpMsg);
            }
            else
            {
                var httpMsg = new HttpRequestMessage(HttpMethod.Get, url);
                response = await httpClient.SendAsync(httpMsg);
            }
            
            if (!response.IsSuccessStatusCode)
            {
                // TODO: Throw exception?? do something here??
            }

            return await response.Content.ReadAsStringAsync();
        }

        private string SendHttpRequest(string url, string requestBody, string contentType)
        {
            var httpClient = new HttpClient();

            HttpResponseMessage response;
            if (!string.IsNullOrEmpty(requestBody))
            {
                var content = new StringContent(requestBody, Encoding.UTF8, contentType);
                var httpMsg = new HttpRequestMessage(HttpMethod.Post, url);
                httpMsg.Content = content;
                response = httpClient.Send(httpMsg);
            }
            else
            {
                var httpMsg = new HttpRequestMessage(HttpMethod.Get, url);
                response = httpClient.Send(httpMsg);
            }

            if (!response.IsSuccessStatusCode)
            {
                // TODO: Throw exception?? do something here??
            }

            return response.Content.ReadAsStringAsync().Result;
        }

    }
}
