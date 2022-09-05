using System.Net.NetworkInformation;

namespace UnitTestExercise
{
    public interface IPingReplyCustom
    {
        IPStatus Status { get; }
    }
}
