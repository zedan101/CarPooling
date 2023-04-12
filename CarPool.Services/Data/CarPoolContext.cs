using CarPool.Services.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Carpool.Services.Data
{
    public class CarPoolContext : DbContext
    {
        public CarPoolContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<OfferedRide> OfferedRide { get; set; }
        public DbSet<BookedRide> BookedRide { get; set; }
        public DbSet<RideLocation> RideLocation { get; set; }

        public DbSet<Locations> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity()
                {
                    UserEmail = "nitish132@gmail.com",
                    Password = "Nitish%%",
                    ProfileImage = "hkdhk",
                    Name = "Nitish",
                    UserId = "gh1"
                },
                new UserEntity()
                {
                    UserEmail = "deepak@gamail.com",
                    Password = "Deepak%%",
                    ProfileImage = "hkdhk",
                    Name = "Deepak",
                    UserId = "gh2"
                }
            );
            modelBuilder.Entity<OfferedRide>().HasData(
                new OfferedRide()
                {
                    Date = new DateTime(2023, 10, 10),
                    Time = 1,
                    AvailableSeats = 2,
                    Price = 100,
                    RideId = "abc@123",
                    UserId = "gh1",
                    CreatedOn = DateTime.Now,
                }
            );
            modelBuilder.Entity<BookedRide>().HasData(
            new BookedRide
            {
                SlNo = 1,
                UserId = "gh2",
                RideId = "abc@123", 
                StartLocationId=2,
                EndLocationId=3,
                BookedOn= DateTime.Now
            },                                                                      
            new BookedRide
            {
                SlNo = 2,
                UserId = "gh2",
                RideId = "abc@123",
                StartLocationId = 2,
                EndLocationId = 3,
                BookedOn= DateTime.Now
            }
);
            modelBuilder.Entity<RideLocation>().HasData(
                new RideLocation()
                {
                    SerialNo= 1,
                    LocationId = 1,
                    RideId = "abc@123",
                    SequenceNum=0
                },
                new RideLocation()
                {
                    SerialNo = 2,
                    LocationId = 2,
                    RideId = "abc@123",
                    SequenceNum = 1
                },
                new RideLocation()
                {
                    SerialNo = 3,
                    LocationId = 3,
                    RideId = "abc@123",
                    SequenceNum = 2
                }
            );
            modelBuilder.Entity<Locations>().HasData(
                new Locations()
                {
                    LocationId = 1,
                    LocationName="delhi"
                },
                new Locations()
                {
                    LocationId = 2,
                    LocationName = "nagpur"
                },
                new Locations()
                {
                    LocationId = 3,
                    LocationName = "mumbai"
                }
             );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder
        .UseLazyLoadingProxies()
        .UseSqlServer("Server=localhost;Database=CarpoolDB;TrustServerCertificate=True;Integrated Security=SSPI;");


    }
}