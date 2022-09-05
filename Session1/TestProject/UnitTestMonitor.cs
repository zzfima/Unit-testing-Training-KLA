using System.Net.NetworkInformation;
using System.Runtime.Intrinsics.X86;
using FluentAssertions;
using Moq;
using UnitTestExercise;

namespace TestProject
{
    [TestClass]
    public class UnitTestMonitor
    {
        private MonitorCustomCustom? _monitor;
        private bool _isAttemptFailed;
        private int _attemptFailedCounter;
        private bool _isConnectionFailed;
        private Mock<IPingReplyCustom> _mockPingReplyCustom;
        private Mock<ITimeoutChecker> _mockTimeoutChecker;

        [TestInitialize]
        public void Initialize()
        {
            //Arrange
            Mock<IConnectivityReporter> mockConnectivityReporter = new Mock<IConnectivityReporter>();
            _mockPingReplyCustom = new Mock<IPingReplyCustom>();
            Mock<IPing> mockPing = new Mock<IPing>();
            Mock<IPingFactory> mockPingFactory = new Mock<IPingFactory>();
            _isAttemptFailed = false;
            _attemptFailedCounter = 0;
            _isConnectionFailed = false;

            mockConnectivityReporter.Setup(m => m.AttemptFailed(It.IsAny<string>())).Callback(() =>
            {
                _isAttemptFailed = true;
                _attemptFailedCounter++;
            });
            mockConnectivityReporter.Setup(m => m.ConnectionFailed(It.IsAny<string>())).Callback(() => _isConnectionFailed = true);
            _mockPingReplyCustom.Setup(m => m.Status).Returns(IPStatus.Success);
            mockPing.Setup(m => m.Send(It.IsAny<string>(), It.IsAny<int>())).Returns(_mockPingReplyCustom.Object);
            mockPingFactory.Setup(m => m.Create()).Returns(mockPing.Object);

            _monitor = new MonitorCustomCustom(mockConnectivityReporter.Object, mockPingFactory.Object);

            _mockTimeoutChecker = new Mock<ITimeoutChecker>();
            var queue = new Queue<int>();
            queue.Enqueue(5);
            queue.Enqueue(4);
            queue.Enqueue(3);
            queue.Enqueue(2);
            queue.Enqueue(1);
            queue.Enqueue(0);
            queue.Enqueue(0);
            queue.Enqueue(0);
            _mockTimeoutChecker.Setup(m => m.Start());
            _mockTimeoutChecker.Setup(m => m.Stop());
            _mockTimeoutChecker.Setup(m => m.MillisecondsLeft).Returns(queue.Dequeue);
        }

        [TestMethod]
        public void ShouldComputerBeAvailable()
        {
            //Arrange at initialize

            //Act
            _monitor?.CheckComputerAvailability(It.IsAny<string>(), new CancellationToken(), _mockTimeoutChecker.Object);

            //Assert
            _isAttemptFailed.Should().BeFalse();
            _isConnectionFailed.Should().BeFalse();
            _attemptFailedCounter.Should().Be(0);
        }

        [TestMethod]
        public void ShouldComputerUnavailable()
        {
            //Arrange at initialize
            _mockPingReplyCustom.Setup(m => m.Status).Returns(IPStatus.Unknown);

            //Act
            _monitor?.CheckComputerAvailability(It.IsAny<string>(), new CancellationToken(), _mockTimeoutChecker.Object);

            //Assert
            _isAttemptFailed.Should().BeTrue();
            _isConnectionFailed.Should().BeTrue();
            _attemptFailedCounter.Should().Be(3);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockTimeoutChecker.Object.Stop();
        }
    }
}