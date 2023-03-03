
namespace CarPool.Models
{
    public class GlobalStorage
    {

        public static List<Users> Users = new List<Users>()
        {
            new Users()
            {
                UserEmail ="nitish@132",
                Password ="Nitish%%",
                ProfileImage="hkdhk",
                UserName="Nitish",
                UserId = "gh1",

            }
        };
        public static List<Rides> Rides = new List<Rides>()
        {
            new Rides()
            {
                Location= new List<string>{"Delhi","Mumbai"},
                Date= "10/10/2023",
                Time= "08:00pm",
                NumberOfSeatsAvailable= "3",
                Price= "$100",
                RideId= "abc@123",
                RideOfferedBy= "gh1",
                RideTakenBy= new List<string>{"",""}
            }
        };
    }
}
