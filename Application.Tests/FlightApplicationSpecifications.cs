using FluentAssertions;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
namespace Application.Tests
{
    public class FlightApplicationSpecifications
    {
        [Theory]
        [InlineData("n@m.com",2)]
        [InlineData("a@a.com",2)]
        public void Books_flights(string passengerEmail,int numOfSeats)
        {
            //given
            var entities = new Entities(new DbContextOptionsBuilder<Entities>()
                .UseInMemoryDatabase("Flights")
                .Options);
            var flight = new Flight(3);
            entities.flights.Add(flight);
            var bookingService = new BookingService(entities: entities);
            //when
            bookingService.Book(new BookDto(flightId: flight.Id, passengerEmail,numOfSeats));
            //then
            bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(new BookingRm(passengerEmail, numOfSeats));
        }
        public class BookingService
        {
            public  Entities Entities { get; set; }
            public BookingService(Entities entities) {
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
        public class BookDto
        {
            public Guid FlightId { get; set; }
            public string PassengerEmail { get; set; }
            public int NumberOfSeats { get; set; }
            public BookDto(Guid flightId,string passengerEmail,int numberOfSeats) { 
                FlightId=flightId;
                PassengerEmail=passengerEmail;
                NumberOfSeats=numberOfSeats;
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