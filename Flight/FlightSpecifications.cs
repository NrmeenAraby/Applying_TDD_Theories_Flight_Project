
using Domain;
using FluentAssertions;
namespace FlightTest
{
    public class FlightSpecifications
    {
        [Theory]
        [InlineData(3,1,2)]
        [InlineData(6,3,3)]
        [InlineData(10,6,4)]
        [InlineData(10,1,9)]
        public void Booking_reduces_the_number_of_seats(int seatCapacity,int numberOfSeats,int remainingNumberOfSeats)
        {
            var flight = new Domain.Flight(seatCapacity:seatCapacity);
            flight.Book("nrmeen@c#.com", numberOfSeats);
            flight.RemainingNumberOfSeats.Should().Be(remainingNumberOfSeats);
        }
       /* [Fact]
        public void Booking_reduces_the_number_of_seats_2()
        {
            var flight = new Domain.Flight(seatCapacity: 6);
            flight.Book("nrmeen@c#.com", 3);
            flight.RemainingNumberOfSeats.Should().Be(3);
        }
        [Fact]
        public void Booking_reduces_the_number_of_seats_3()
        {
            var flight = new Domain.Flight(seatCapacity: 10);
            flight.Book("nrmeen@c#.com", 4);
            flight.RemainingNumberOfSeats.Should().Be(6);
        }*/

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
        [Fact]
        public void Books_flights_successfully()
        {
            //given 
            var flight=new Domain.Flight(seatCapacity:3);
            //when
            var error=flight.Book("nrmeen@c#.com", 1);
            //then 
            error.Should().BeNull();
        }
        [Fact]
        public void Remembers_booking()
        {
            //given
            var flight = new Domain.Flight(seatCapacity: 15);
            //when 
            flight.Book("n@m.com", 5);
            //then
            flight.BookingList.Should().ContainEquivalentOf(new Booking("n@m.com", 5));
        }
    }
}