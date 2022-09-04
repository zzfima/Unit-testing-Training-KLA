using FluentAssertions;
using UnitTestExercise;

namespace TestProject
{
    [TestClass]
    public class UnitTestReservationCrud
    {
        private FileInfo _file;

        [TestMethod]
        public void ReservationShouldPersist()
        {
            var reservationPersister = new ReservationCrud();
            var random = new Random();
            var reservation = new Reservation(random.Next(), "Alex", 5);
            _file = reservationPersister.Persist(reservation);
            File.Exists(_file.FullName).Should().BeTrue();

        }

        [TestMethod]
        public void ReservationShouldPersistAndRead()
        {
            var reservationPersister = new ReservationCrud();
            var random = new Random();
            var reservation = new Reservation(random.Next(), "Alex", 5);
            _file = reservationPersister.Persist(reservation);
            var readReservation = reservationPersister.Read(_file);
            reservation.Should().BeEquivalentTo(readReservation);
        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete(_file.FullName);
        }
    }
}