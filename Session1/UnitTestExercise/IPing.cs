using System;
using System.Net.NetworkInformation;

namespace UnitTestExercise
{
    public interface IPing : IDisposable
    {
        PingReply Send(string ip, int milliseconds);
    }
}