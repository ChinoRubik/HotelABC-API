namespace HotelABC_API.Models.Domain
{
    public class Room
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Characteristics { get; set; }

        public double Price { get; set; }

        public virtual List<Image> Images { get; set; }

    }
}
