
using Domain;
using FluentAssertions;
namespace FlightTest
{
    public class FlightSpecifications
    {
        [Fact]
        public void Booking_reduces_the_number_of_seats()
        {
            var flight = new Domain.Flight(seatCapacity:3);
            flight.Book("nrmeen@c#.com", 1);
            flight.RemainingNumberOfSeats.Should().Be(2);
        }

        [Fact]
        public void Avoids_overbooking()
        {
            //Given
            var flight =new Domain.Flight(seatCapacity:3);
            //when
            var error = flight.Book("nrmeen@c#.com", 4);
            //Then
            error.Should().BeOfType<OverbookingError>();
        }
    }
}