using System.Net.NetworkInformation;
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
        }

        [TestMethod]
        public void ShouldComputerBeAvailable()
        {
            //Arrange at initialize
            var mockTimeOut = new TimeotChecker(new TimeSpan(0,0,1));

            //Act
            _monitor?.CheckComputerAvailability(It.IsAny<string>(), new CancellationToken(), mockTimeOut);

            //Assert
            _isAttemptFailed.Should().BeFalse();
            _isConnectionFailed.Should().BeFalse();
            _attemptFailedCounter.Should().Be(0);
        }

        [TestMethod]
        public void ShouldComputerUnaavailable()
        {
            //Arrange at initialize
            _mockPingReplyCustom.Setup(m => m.Status).Returns(IPStatus.Unknown);
            var mockTimeOut = new TimeotChecker(new TimeSpan(0,0,1));

            //Act
            _monitor?.CheckComputerAvailability(It.IsAny<string>(), new CancellationToken(), mockTimeOut);

            //Assert
            _isAttemptFailed.Should().BeTrue();
            _isConnectionFailed.Should().BeTrue();
            //_attemptFailedCounter.Should().Be(0);
        }

        [TestCleanup]
        public void Cleanup()
        {

        }
    }
}