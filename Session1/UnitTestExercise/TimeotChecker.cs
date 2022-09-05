using System;
using System.Diagnostics;

namespace UnitTestExercise
{
    public class TimeotChecker : ITimeoutChecker
    {
        private readonly TimeSpan _timeOut;
        private Stopwatch _timeUsed;

        public TimeotChecker(TimeSpan timeOut)
        {
            _timeOut = timeOut;
        }
        public int MillisecondsLeft => (int)(_timeOut - _timeUsed.Elapsed).TotalMilliseconds;

        public void Start()
        {
            _timeUsed = Stopwatch.StartNew();
            var millisecondsLeft = (int)_timeOut.TotalMilliseconds;
        }

        public void Stop()
        {
            _timeUsed.Stop();
        }
    }
}
