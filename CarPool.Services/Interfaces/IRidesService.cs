using CarPool.Models;

namespace CarPool.Services.Interfaces
{
    public interface IRidesService
    {
        public Task<List<Ride>> MatchRides(string userId,DateTime date, int time, string startLocation, string destination);
        public Task<bool> OfferRide(Ride ride);
        public Task<List<Ride>> BookedRideHistory(string userId);
        public Task<List<Ride>> OfferedRideHistory(string userId);
        public Task<bool> BookingRide(string userId,int seats,string rideId);

    }
}
