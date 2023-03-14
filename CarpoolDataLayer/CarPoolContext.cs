using CarPool.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarpoolDataLayer
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity()
                {
                    UserEmail = "nitish@132",
                    Password = "Nitish%%",
                    ProfileImage = "hkdhk",
                    Name = "Nitish",
                    UserId = "gh1"
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
                    UserId = "gh1"
                }
            );
            modelBuilder.Entity<BookedRide>().HasData(
            new BookedRide
            {
                SlNo = 1,
                UserId = "gh1",
                RideId = "abc@123"  
            },                                                                      
            new BookedRide
            {
                SlNo = 2,
                UserId = "gh1",
                RideId = "abc@123"
            }
);
            modelBuilder.Entity<RideLocation>().HasData(
                new RideLocation()
                {
                    SerialNo= 1,
                    Location = "Delhi",
                    RideId = "abc@123",
                    SequenceNum=0
                },
                new RideLocation()
                {
                    SerialNo = 2,
                    Location = "Mumbai",
                    RideId = "abc@123",
                    SequenceNum = 1
                },
                new RideLocation()
                {
                    SerialNo = 3,
                    Location = "Nagpur",
                    RideId = "abc@123",
                    SequenceNum = 2
                }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder
        .UseLazyLoadingProxies()
        .UseSqlServer("Server=localhost;Database=CarpoolDB;TrustServerCertificate=True;Integrated Security=SSPI;");


    }
}