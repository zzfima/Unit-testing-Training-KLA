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

        public void CheckComputerAvailability(string ip, CancellationToken cancellationToken, ITimeoutChecker timeout)
        {
            timeout.Start();
            using (var ping = _pingFactory.Create())
            {
                while (timeout.MillisecondsLeft > 0 && !cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var reply = ping.Send(ip, timeout.MillisecondsLeft);
                        if (reply?.Status == IPStatus.Success)
                        {
                            return;
                        }

                        _connectivityReporter.AttemptFailed(ip);
                    }
                    catch
                    {
                    }
                }
            }
            _connectivityReporter.ConnectionFailed(ip);
        }
    }
}