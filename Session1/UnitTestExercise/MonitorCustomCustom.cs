using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading;

namespace UnitTestExercise
{
    public class MonitorCustomCustom : IMonitorCustom
    {
        private readonly IConnectivityReporter _connectivityReporter;
        private readonly IPingFactory _pingFactory;

        public MonitorCustomCustom(IConnectivityReporter connectivityReporter, IPingFactory pingFactory)
        {
            _connectivityReporter = connectivityReporter;
            _pingFactory = pingFactory;
        }

        public void CheckComputerAvailability(string ip, CancellationToken cancellationToken, TimeSpan timeout)
        {
            var timeUsed = Stopwatch.StartNew();
            var millisecondsLeft = (int)timeout.TotalMilliseconds;

            using (var ping = _pingFactory.Create())
            {
                while (millisecondsLeft > 0 && !cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var reply = ping.Send(ip, millisecondsLeft);
                        if (reply?.Status == IPStatus.Success)
                        {
                            return;
                        }

                        _connectivityReporter.AttemptFailed(ip);
                    }
                    catch
                    {
                    }
                    millisecondsLeft = (int)(timeout - timeUsed.Elapsed).TotalMilliseconds;
                }
            }
            _connectivityReporter.ConnectionFailed(ip);
        }
    }
}