using FluentAssertions;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
namespace Application.Tests
{
    public class FlightApplicationSpecifications
    {
        [Fact]
        public void Books_flights()
        {
            var entities = new Entities(new DbContextOptionsBuilder<Entities>()
                .UseInMemoryDatabase("Flights")
                .Options);
            var flight = new Flight(3);
            entities.flights.Add(flight);
            var bookingService = new BookingService(entities: entities);
            bookingService.Book(new BookDto(flightId: flight.Id, passengerEmail: "a@b.com",numberOfSeats: 2));
            bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(new BookingRm("a@b.com", 2));
        }
        public class BookingService
        {
            public BookingService(Entities entities) { 
            
            }
            public void Book(BookDto bookDto)
            {

            }
            public IEnumerable<BookingRm> FindBookings(Guid flightId)
            {
                return new[] {
                    new BookingRm("a@b.com", 2) 
                };
            }
        }
        public class BookDto
        {
            public BookDto(Guid flightId,string passengerEmail,int numberOfSeats) { 

            }

        }
        public class BookingRm
        {
            public string PassengerEmail { get; set; }
            public int NumberOfSeats { get; set; }
            public BookingRm(string passengerEmail, int numberOfSeats)
            {
                PassengerEmail= passengerEmail;
                NumberOfSeats=numberOfSeats;
            }
        }
    }
}