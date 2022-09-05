using System;
using System.Threading;

namespace UnitTestExercise
{
    public interface IMonitorCustom
    {
        void CheckComputerAvailability(string ip, CancellationToken cancellationToken, ITimeoutChecker timeout);
    }
}