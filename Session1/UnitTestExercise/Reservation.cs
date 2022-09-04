using System;
using System.IO;
using System.Xml.Serialization;

namespace UnitTestExercise
{
    [Serializable]
    public class Reservation
    {
        #region Constructor

        public Reservation()
        {

        }

        public Reservation(int id, string reservatorName, int quantity)
        {
            Id = id;
            ReservatorName = reservatorName;
            Quantity = quantity;
            PersistReservation();
        }

        #endregion

        #region Properties

        public int Id { get; }

        public string ReservatorName { get; }

        public int Quantity { get; }

        #endregion

        #region Methods

        private void PersistReservation()
        {
            var serializer = new XmlSerializer(typeof(Reservation));
            using (var stream = new FileStream("Reservation " + Id, FileMode.CreateNew))
            {
                serializer.Serialize(stream, this);
            }
        }

        #endregion
    }
}