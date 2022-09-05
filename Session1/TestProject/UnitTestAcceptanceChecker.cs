using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using UnitTestExercise;

namespace TestProject
{

    [TestClass]
    public class UnitTestAcceptanceChecker
    {
        [TestMethod]
        public void AcceptanceShouldPass()
        {
            //Arrange
            IEnumerable<Reservation> existingReservations = new List<Reservation>()
            {
                new Reservation(11, "Benny", 22),
                new Reservation(12, "Katy", 11),
                new Reservation(13, "Danny", 3),
            };
            var newReservation = new Reservation(33, "Barry", 1);
            var restaurant = new Restaurant(120);
            var acceptanceChecker = new AcceptanceChecker();

            //Act + assert
            acceptanceChecker.CanAccept(existingReservations, newReservation, restaurant).Should().BeTrue();
        }

        [TestMethod]
        public void AcceptanceShouldFail()
        {
            //Arrange
            IEnumerable<Reservation> existingReservations = new List<Reservation>()
            {
                new Reservation(11, "Benny", 44),
                new Reservation(12, "Katy", 44),
                new Reservation(13, "Danny", 3),
            };
            var newReservation = new Reservation(33, "Barry", 44);
            var restaurant = new Restaurant(120);
            var acceptanceChecker = new AcceptanceChecker();

            //Act + assert
            acceptanceChecker.CanAccept(existingReservations, newReservation, restaurant).Should().BeFalse();
        }
    }
}