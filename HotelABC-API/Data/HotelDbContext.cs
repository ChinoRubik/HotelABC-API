using HotelABC_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelABC_API.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<ImageType> ImageTypes { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seeding data for ImageTypes

            var ImagesTypes = new List<ImageType>()
            {
                new ImageType()
                {
                    Id = Guid.Parse("3897b275-7a3f-4a84-a620-105b9b0eb89a"),
                    Type = "room",
                },
                new ImageType()
                {
                    Id = Guid.Parse("e4567686-1b4d-483d-a374-9e99306c8e7b"),
                    Type = "carousel",
                },
                new ImageType()
                {
                    Id = Guid.Parse("de63304d-8500-4570-8333-abb077e5a23f"),
                    Type = "food",
                },
                new ImageType()
                {
                    Id = Guid.Parse("8929b4bf-5be3-4002-8ad6-b9f46f782f16"),
                    Type = "offers",
                },
            };

            modelBuilder.Entity<ImageType>().HasData(ImagesTypes);


            //Making relation between images en rooms
            modelBuilder.Entity<Image>()
               .HasOne(image => image.Room)
               .WithMany(room => room.Images)
               .HasForeignKey(image => image.RelativeRelationId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
