using CarPool.Models;

namespace CarPool.Services.Interfaces
{
    public interface IRidesService
    {
        public Task<IEnumerable<Ride>> MatchRides(MatchRideReq matchReq);
        public Task<bool> OfferRide(Ride ride);
        public Task<IEnumerable<Ride>> BookedRideHistory();
        public Task<IEnumerable<Ride>> OfferedRideHistory();
        public Task<bool> BookingRide(int seats,string rideId);

    }
}
