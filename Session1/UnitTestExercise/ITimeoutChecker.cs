namespace UnitTestExercise
{
    public interface ITimeoutChecker
    {
        int MillisecondsLeft { get; }
        void Start();
        void Stop();
    }
}
