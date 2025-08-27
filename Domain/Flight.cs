namespace Domain
{
    public class Flight
    {
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
            return null;
        }
    }
}
