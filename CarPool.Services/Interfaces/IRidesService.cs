using CarPool.Models;

namespace CarPool.Services.Interfaces
{
    public interface IRidesService
    {
        public Task<List<Ride>> GetMatches(DateTime date, int time, string startLocation, string destination);
        public Task<bool> OfferRide(Ride ride);
        public Task<List<Ride>> GetBookedRideHistory(string userId);
        public Task<List<Ride>> GetOfferedRideHistory(string userId);
        public Task<bool> Booking(string userId,int seats,string rideId);

    }
}
