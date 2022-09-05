using FluentAssertions;
using UnitTestExercise;

namespace TestProject
{
    [TestClass]
    public class UnitTestReservationCrud
    {
        private FileInfo? _file;
        private Reservation? _reservation;
        private ReservationCrud? _reservationPersister;

        [TestInitialize]
        public void Initialize()
        {
            //Arrange
            var random = new Random();
            _reservation = new Reservation(random.Next(), "Alex", 5);
            _reservationPersister = new ReservationCrud();
        }

        [TestMethod]
        public void PersistShouldCreateFile()
        {
            //Act
            _file = _reservationPersister.Persist(_reservation);

            //Assert
            File.Exists(_file.FullName).Should().BeTrue();

        }

        [TestMethod]
        public void ReservationShouldPersistAndRead()
        {
            //Act
            _file = _reservationPersister.Persist(_reservation);
            var readReservation = _reservationPersister.Read(_file);

            //Assert
            _reservation.Should().BeEquivalentTo(readReservation);
        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete(_file.FullName);
        }
    }
}