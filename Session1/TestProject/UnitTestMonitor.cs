using System.Net.NetworkInformation;
using FluentAssertions;
using Moq;
using UnitTestExercise;

namespace TestProject
{
    [TestClass]
    public class UnitTestMonitor
    {
        private Mock<IConnectivityReporter>? _mockConnectivityReporter;
        private Mock<IPingFactory>? _mockPingFactory;

        [TestInitialize]
        public void Initialize()
        {
            //Arrange
            _mockConnectivityReporter = new Mock<IConnectivityReporter>();
            _mockPingFactory = new Mock<IPingFactory>();
        }

        [TestMethod]
        public void PersistShouldCreateFile()
        {
            //Arrange
            Mock<IConnectivityReporter> mockConnectivityReporter = new Mock<IConnectivityReporter>();
            Mock<IPingReplyCustom> mockPingReplyCustom = new Mock<IPingReplyCustom>();
            Mock<IPing> mockPing = new Mock<IPing>();
            Mock<IPingFactory> mockPingFactory = new Mock<IPingFactory>();
            bool isAttemptFailed = false;
            int attemptFailedCounter = 0;
            bool isConnectionFailed = false;

            mockConnectivityReporter.Setup(m => m.AttemptFailed(It.IsAny<string>())).Callback(() =>
            {
                isAttemptFailed = true;
                attemptFailedCounter++;
            });
            mockConnectivityReporter.Setup(m => m.ConnectionFailed(It.IsAny<string>())).Callback(() => isConnectionFailed = true);
            mockPingReplyCustom.Setup(m => m.Status).Returns(IPStatus.Success);
            mockPing.Setup(m => m.Send(It.IsAny<string>(), It.IsAny<int>())).Returns(mockPingReplyCustom.Object);
            mockPingFactory.Setup(m => m.Create()).Returns(mockPing.Object);

            var monitor = new MonitorCustomCustom(mockConnectivityReporter.Object, mockPingFactory.Object);

            //Act
            monitor.CheckComputerAvailability(It.IsAny<string>(), new CancellationToken(), new TimeSpan(0, 0, 1));

            //Assert
            isAttemptFailed.Should().BeFalse();
            isConnectionFailed.Should().BeFalse();
            attemptFailedCounter.Should().Be(0);
        }

        [TestCleanup]
        public void Cleanup()
        {

        }
    }
}