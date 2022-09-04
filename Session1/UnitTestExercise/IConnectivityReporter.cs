namespace UnitTestExercise
{
    public interface IConnectivityReporter
    {
        void AttemptFailed(string ip);
        void ConnectionFailed(string ip);
    }
}