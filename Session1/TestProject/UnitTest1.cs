using UnitTestExercise;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ReservationShouldPersist()
        {
            var reservation = new Reservation(1, "Alex B.", 4);
        }
    }
}