using System.Collections.Generic;
using System.Linq;

namespace UnitTestExercise
{
    public class AcceptanceChecker : IAcceptanceChecker
    {
        public bool CanAccept(IEnumerable<Reservation> existingReservations, Reservation newReservation, Restaurant restaurant)
        {
            var reservedSeats = existingReservations.Sum(r => r.Quantity);

            if (restaurant.Capacity > reservedSeats + newReservation.Quantity)
                return true;

            return false;
        }
    }
}