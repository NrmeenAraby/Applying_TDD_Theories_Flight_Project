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

        [Theory]
        [InlineData(3)]
        [InlineData(10)]
        public void Cancels_booking(int initalCapacity)
        {
            //given
            var entities = new Entities(new DbContextOptionsBuilder<Entities>()
                .UseInMemoryDatabase("Flights")
                .Options);
            var bookingService = new BookingService(entities: entities);
            var flight = new Flight(initalCapacity);
            entities.flights.Add(flight);
            bookingService.Book(new BookDto(flight.Id,"m@m.com",2));
            //when 
            bookingService.CancelBooking( new CancelBookingDto(flight.Id,"m@m.com",2));
            //Then
            bookingService.GetRemainingNumOfSeats(flight.Id).Should().Be(initalCapacity);
        }


    }
}

