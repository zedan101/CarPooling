
namespace CarPool.Models
{
    public class GlobalStorage
    {

        public static List<User> Users = new List<User>()
        {
            new User()
            {
                UserEmail ="nitish@132",
                Password ="Nitish%%",
                ProfileImage="hkdhk",
                UserName="Nitish",
                UserId ="gh1"

            }
        };
        public static List<Ride> Rides = new List<Ride>()
        {
            new Ride()
            {
                Location= new List<string>{"Delhi","Mumbai"},
                Date= new DateTime(2023 , 10 ,10),
                Time= 1,
                NumberOfSeatsAvailable= 2,
                Price= 100,
                RideId= "abc@123",
                RideOfferedBy= "gh1",
                RideTakenBy= new List<string>{"gh1",}
            }
        };
    }
}
