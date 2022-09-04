using System.IO;
using System.Xml.Serialization;

namespace UnitTestExercise
{
    public class Reservation
    {
        public Reservation(int id, string reservationistName, int quantity)
        {
            Id = id;
            ReservationistName = reservationistName;
            Quantity = quantity;
            PersistReservation();
        }    

        public int Id { get; }

        public string ReservationistName { get; }

        public int Quantity { get; }

        private void PersistReservation()
        {
            var serializer = new XmlSerializer(typeof(Reservation));
            using (var stream = new FileStream("Reservation " + Id, FileMode.CreateNew))
            {               
                serializer.Serialize(stream, this);
            }
        }
    }
}
