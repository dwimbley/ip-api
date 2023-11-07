using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dns.IpTrace.Core;
using Dns.IpTrace.Core.Models;
using Dns.IpTrace.Jobs.Models;

namespace Dns.IpTrace.Jobs.Core
{
    public class IpTraceJob
    {
        public List<IpAddressModel> TraceIps(List<IpRequest> ipsToTrace)
        {
            var tracer = new IpTracerFactory(new TraceHttpClient()).Get();
            var hasIps = true;

            var stopwatch = new Stopwatch();

            Console.WriteLine($"Total filtered logs to trace - {ipsToTrace.Count()}");
            var mainCounter = 0;
            var allResults = new List<IpAddressModel>();

            while (hasIps)
            {
                stopwatch.Start();

                var requestResults = new List<IpAddressModel>();
                var ipCounter = 0;
                var hasMoreResults = true;
                for (int i = 0; i < 15; i++)
                {
                    if (!hasMoreResults)
                    {
                        continue;
                    }

                    Console.WriteLine($"Making batch ip request #{i}");
                    Console.WriteLine($"IP Count to skip {ipCounter}");

                    var ipAddresses = ipsToTrace.Skip(ipCounter).Take(100).Select(m => m.IpAddress).ToList();

                    if (!ipAddresses.Any())
                    {
                        Console.WriteLine("No more IPs to trace, break out of request loop");
                        hasMoreResults = false;
                        continue;
                    }

                    Console.WriteLine($"Total IPs in batch request #{i} - {ipAddresses.Count}");

                    var results = tracer.Get(ipAddresses);

                    Console.WriteLine($"Batch #{i} result count {results.Count}");

                    requestResults.AddRange(results);
                    allResults.AddRange(results);

                    ipCounter += 100;
                }

                if (!requestResults.Any())
                {
                    Console.WriteLine("No more IPs to trace, break out of main loop");
                    hasIps = false;
                    break;
                }

                Console.WriteLine($"Total Ip Trace Results Count {requestResults.Count}");
                
                var hasMatchingIps = false;
                
                Console.WriteLine($"Had any matching ips? {hasMatchingIps}");
              
                stopwatch.Stop();

                // Due to API limitations, need to throttle the process if it has ran too quickly..
                // Limits 15 requests every minute, performs all 15 requests initially and then 
                // processes the results - dmw 6/21/2022
                double ticks = stopwatch.ElapsedTicks;
                double seconds = ticks / Stopwatch.Frequency;
                int adjustedSeconds = (int)Math.Floor(seconds);
                int timeRemaining = 60 - adjustedSeconds;

                Console.WriteLine($"Process stats for round #{mainCounter}");
                Console.WriteLine($"Total ticks {ticks}");
                Console.WriteLine($"Total seconds {seconds}");
                Console.WriteLine($"Total adjusted seconds {adjustedSeconds}");
                Console.WriteLine($"Time remaining {timeRemaining}");
                Console.WriteLine($"Has More Results? {hasMoreResults}");

                if (timeRemaining > 0 && hasMoreResults)
                {
                    var timeToWait = timeRemaining * 1000;
                    Console.WriteLine($"Process for round {mainCounter} too fast, start wait for {timeRemaining} second(s)");
                    Thread.Sleep(timeToWait);
                }

                mainCounter++;
            }

            Console.WriteLine("Impressions log ip address trace complete");

            return allResults;
        }
    }
}
