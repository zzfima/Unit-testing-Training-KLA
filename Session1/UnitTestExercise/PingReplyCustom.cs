using System.Net.NetworkInformation;

namespace UnitTestExercise
{
    public class PingReplyCustom : IPingReplyCustom
    {
        private PingReply _pingReply;

        public PingReplyCustom(PingReply pingReply)
        {
            _pingReply = pingReply;
        }

        public IPStatus Status => _pingReply.Status;
    }
}
