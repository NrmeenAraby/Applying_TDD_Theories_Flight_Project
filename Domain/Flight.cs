namespace Domain
{
    public class Flight
    {
        List<Booking> bookingList { get; set; } = new List<Booking>();
        public IEnumerable<Booking> BookingList => bookingList;
        public int RemainingNumberOfSeats{ get; set; }
        public Flight(int seatCapacity)
        {
            RemainingNumberOfSeats = seatCapacity;
        }
        public object? Book (string PassengerEmail,int bookedSeats)
        {
            if (bookedSeats > RemainingNumberOfSeats)
                return new OverbookingError();
            RemainingNumberOfSeats-= bookedSeats;
            bookingList.Add(new Booking(PassengerEmail, bookedSeats));
            return null;
        }
    }
}
