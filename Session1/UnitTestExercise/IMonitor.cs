using System;
using System.Threading;

namespace UnitTestExercise
{
    public interface IMonitor
    {
        void CheckComputerAvailability(string ip, CancellationToken cancellationToken, TimeSpan timeout);
    }
}