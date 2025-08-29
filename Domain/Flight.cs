namespace Domain
{
    public class Flight
    {
        public List<Booking> BookingList { get; set; } = new List<Booking>();
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
            BookingList.Add(new Booking(PassengerEmail, bookedSeats));
            return null;
        }
    }
}
