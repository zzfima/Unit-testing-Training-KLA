using FluentAssertions;
using UnitTestExercise;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
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
    }
}