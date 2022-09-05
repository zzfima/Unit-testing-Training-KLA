using System;

namespace UnitTestExercise
{
    public interface IPing : IDisposable
    {
        IPingReplyCustom Send(string ip, int milliseconds);
    }
}