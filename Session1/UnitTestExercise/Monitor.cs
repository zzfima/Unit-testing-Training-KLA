using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading;

namespace UnitTestExercise
{
    class Monitor
    {
        private readonly IConnectivityReporter connectivityReporter;
        private readonly IPingFactory pingFactory;

        public Monitor(IConnectivityReporter connectivityReporter, IPingFactory pingFactory)
        {
            this.connectivityReporter = connectivityReporter;
            this.pingFactory = pingFactory;
        }

        public void CheckComputerAvailability(string ip, CancellationToken cancellationToken, TimeSpan timeout)
        {
            Stopwatch timeUsed = Stopwatch.StartNew();
            int millisecsLeft = (int)timeout.TotalMilliseconds;

            using (var ping = pingFactory.Create())
            {
                while (millisecsLeft > 0 && !cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        PingReply reply = ping.Send(ip, millisecsLeft);
                        if (reply?.Status == IPStatus.Success)
                        { 
                            return; 
                        }

                        connectivityReporter.AttemptFailed(ip);
                    }
                    catch
                    {
                    }
                    millisecsLeft = (int)(timeout - timeUsed.Elapsed).TotalMilliseconds;
                }
            }
            connectivityReporter.ConnectionFailed(ip);
        }
    }
}
