using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;
using CarPool.Services.Interfaces;

namespace CarPool.Services
{
    public class RidesService : IRidesService
    {

        /// <summary>
        /// Method to get user input and match the input with available rides and send back the matched rides.
        /// </summary>
        /// <param name="date">Date of the ride</param>
        /// <param name="time">Time of the ride</param>
        /// <param name="startLocation">Start point of the ride</param>
        /// <param name="destination">Destination</param>
        /// <returns>Returs list of rides matching the conditions</returns>
            public List<Rides> GetMatches(string date, string time, string startLocation, string destination)
            {
                return GlobalStorage.Rides.FindAll(rides => rides.Date == date && rides.Time == time && rides.Location.Any(loc=> loc == startLocation) && rides.Location.Any(loc => loc == destination) && rides.Location.FindIndex(loc=> loc == startLocation) < rides.Location.FindIndex(loc => loc == destination)).ToList();
            }


        /// <summary>
        /// Method to add ride for listing.It Takes user input as a instence of ride  class and adds it to the Rides list. 
        /// </summary>
        /// <param name="ride">Details of ride to be Added</param>
        /// <returns>Success or not response as bool</returns>
            public bool OfferRide(Rides ride)
            {
                GlobalStorage.Rides.Add(ride);
                return true;
            }


        /// <summary>
        /// Method to Get History of Rides.It Takes User Id And Compares It With both RideTakenBy and RideOfferedBy members of ride class
        /// and returns the list of matches as response.
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <returns>List of Rides that matches the querry</returns>
            public List<Rides> GetRideHistory(string userEmail)
            {
                return GlobalStorage.Rides.Where(ride => ride.RideTakenBy.Any(Email => Email==userEmail) || ride.RideOfferedBy == userEmail).ToList();
            }


        /// <summary>
        /// Method to Book a ride. It takes userEmail, seats and rideId as arguments. First checks the values for null than gets the ride being booked using 
        /// rideId and assigns userEmail to rideTakenBy field of the ride.
        /// </summary>
        /// <param name="userEmail">Email of User</param>
        /// <param name="seats">No of seats being booked</param>
        /// <param name="rideId">Id of the ride being booked</param>
        /// <returns>Success or not response as bool</returns>
            public bool Booking(string userEmail , int seats , string rideId)
            {
                if(userEmail!=null && seats!=0 && rideId != null)
                {
                    var ride = GlobalStorage.Rides.FirstOrDefault(ride=> ride.RideId== rideId);
                    for(int i = 1; i < seats; i++)
                    {
                        ride.RideTakenBy.Add(userEmail);
                    }     
                    return true;
                }
                else
                {
                    return false;
                }
            }
    }
}

