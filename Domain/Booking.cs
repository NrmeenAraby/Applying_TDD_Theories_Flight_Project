using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Booking
    {
        public string Email { get; set; }
        public int NumberOfSeats { get; set; }

        [Obsolete("Needed by EF")]
        protected Booking() { }
        public Booking(string email, int seats)
        {
            Email = email;
            NumberOfSeats = seats;
        }
    }
}
