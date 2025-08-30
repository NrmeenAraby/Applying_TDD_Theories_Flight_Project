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
        
       
       
    }
}