using System.Collections.Generic;
using System.Linq;

namespace UnitTestExercise
{
    public class Restaurant
    {
        public Restaurant(int capacity)
        {
            Capacity = capacity;
        }

        public int Capacity { get; }

        public bool CanAccept(IEnumerable<Reservation> existingReservations, Reservation newReservation)
        {
            var reservedSeats = existingReservations.Sum(r => r.Quantity);

            if (Capacity > reservedSeats + newReservation.Quantity)
                return true;

            return false;
        }
    }
}