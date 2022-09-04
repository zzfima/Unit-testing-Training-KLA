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
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        public string ReservatorName { get; set; }

        public int Quantity { get; set; }

        #endregion
    }
}