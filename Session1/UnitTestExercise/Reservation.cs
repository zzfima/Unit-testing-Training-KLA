using System.IO;
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
}
