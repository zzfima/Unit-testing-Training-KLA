using System.IO;

namespace UnitTestExercise
{
    public interface IReservationCrud
    {
        FileInfo Persist(Reservation reservation);
        Reservation Read(FileInfo file);
    }
}