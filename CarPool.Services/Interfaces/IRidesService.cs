using CarPool.Models;

namespace CarPool.Services.Interfaces
{
    public interface IRidesService
    {
        public List<Rides> GetMatches(string date, string time, string startLocation, string destination);
        public bool OfferRide(Rides ride);
        public List<Rides> GetRideHistory(string userId);
        public bool Booking(string userId,int seats,string rideId);

    }
}
