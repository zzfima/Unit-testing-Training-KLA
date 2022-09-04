using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace UnitTestExercise
{
    public class Reservation
    {
        public Reservation(int id, string reserverName, int quantity)
        {
            Id = id;
            ReserverName = reserverName;
            Quantity = quantity;
            PersisteReservation();
        }    

        public int Id { get; }

        public string ReserverName { get; }

        public int Quantity { get; }

        private void PersisteReservation()
        {
            var serializer = new XmlSerializer(typeof(Reservation));
            using (var stream = new FileStream("Reservation " + Id, FileMode.CreateNew))
            {               
                serializer.Serialize(stream, this);
            }
        }
    }

    public class Restaurant
    {
        public Restaurant(int capacity)
        {
            Capacity = capacity;
        }

        public int Capacity { get; }

        public bool CanAccept(IEnumerable<Reservation> existingReeservations, Reservation newReservation)
        {
            var reservedSeats = existingReeservations.Sum(r => r.Quantity);

            if (Capacity > reservedSeats + newReservation.Quantity)
                return true;

            return false;
        }
    }
}
