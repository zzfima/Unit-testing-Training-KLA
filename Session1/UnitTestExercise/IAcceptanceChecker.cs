using System.Collections.Generic;

namespace UnitTestExercise
{
    public interface IAcceptanceChecker
    {
        bool CanAccept(IEnumerable<Reservation> existingReservations, Reservation newReservation, Restaurant restaurant);
    }
}
