
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
        [Theory]
        [InlineData(3,1,1,3)]
        [InlineData(6,2,1,5)]
        [InlineData(5,4,4,5)]
        public void Canceling_bookings_frees_up_the_seats(int numOfSeats,int numOfBookingSeats,int canceldSeats,int remainingSeats)
        {
            // given
            var flight = new Flight(seatCapacity: numOfSeats);
            flight.Book("nrmeen@c#",numOfBookingSeats);

            //when 
            flight.CancelBooking("nrmeen@c#", canceldSeats);

            //then
            flight.RemainingNumberOfSeats.Should().Be(remainingSeats);

        }
        [Fact]
        public void Doesnt_cancel_bookings_for_passengers_who_have_not_booked()
        {
            var flight = new Flight(3);
            var error=flight.CancelBooking("nrmeen@c#.com",2);
            error.Should().BeOfType<BookingNotFoundError>();  
        }
        [Fact]
        public void Return_null_when_successfully_cancels_a_booking()
        {
            var flight = new Flight(3);
            flight.Book("nrmeen@c#.com", 1);
            var error=flight.CancelBooking("nrmeen@c#.com", 1);
            error.Should().BeNull();
        }
    }
}