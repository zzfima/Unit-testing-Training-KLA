using System.IO;

namespace UnitTestExercise.Reservation
{
    public interface IReservationCrud
    {
        FileInfo Persist(Reservation reservation);
        Reservation Read(FileInfo file);
    }
}