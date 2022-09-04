using FluentAssertions;
using UnitTestExercise;

namespace TestProject
{
    [TestClass]
    public class UnitTestReservationCrud
    {
        [TestMethod]
        public void ReservationShouldPersist()
        {
            var reservationPersister = new ReservationCrud();
            var random = new Random();
            var reservation = new Reservation(random.Next(), "Alex", 5);
            var file = reservationPersister.Persist(reservation);
            File.Exists(file.FullName).Should().BeTrue();
            File.Delete(file.FullName);
        }

        [TestMethod]
        public void ReservationShouldPersistAndRead()
        {
            var reservationPersister = new ReservationCrud();
            var random = new Random();
            var reservation = new Reservation(random.Next(), "Alex", 5);
            var file = reservationPersister.Persist(reservation);
            var readReservation = reservationPersister.Read(file);
            reservation.Should().BeEquivalentTo(readReservation);
            File.Delete(file.FullName);
        }
    }
}