using System.IO;
using System.Xml.Serialization;

namespace UnitTestExercise.Reservation
{
    public class ReservationCrud : IReservationCrud
    {
        public FileInfo Persist(Reservation reservation)
        {
            var serializer = new XmlSerializer(typeof(Reservation));
            var path = new FileInfo("Reservation " + reservation.Id);
            using (var stream = new FileStream(path.FullName, FileMode.CreateNew))
            {
                serializer.Serialize(stream, reservation);
            }
            return path;
        }

        public Reservation Read(FileInfo file)
        {
            var serializer = new XmlSerializer(typeof(Reservation));
            using (var stream = new FileStream(file.FullName, FileMode.Open))
            {
                return serializer.Deserialize(stream) as Reservation;
            }
        }
    }
}
