using Data;

namespace Application
{
    public class BookingService
    {
        public Entities Entities { get; set; }
        public BookingService(Entities entities)
        {
            Entities = entities;
        }
        public void Book(BookDto bookDto)
        {
            var flight = Entities.flights.Find(bookDto.FlightId);
            flight.Book(bookDto.PassengerEmail, bookDto.NumberOfSeats);
            Entities.SaveChanges();
        }
        public IEnumerable<BookingRm> FindBookings(Guid flightId)
        {
            return Entities.flights.Find(flightId)
                .BookingList.Select(booking => new BookingRm(booking.Email, booking.NumberOfSeats));
        }
    }
}
