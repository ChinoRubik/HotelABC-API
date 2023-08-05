namespace HotelABC_API.Models.Domain
{
    public class RoomReservation
    {
        public Guid Id { get; set; }

        public Guid roomId { get; set; }

        public int? hoursHired { get; set; }

        public int? extraHoursHired { get; set; }

        public DateTime? checkIn { get; set; }

        public DateTime? checkOut { get; set; }

        public string? status { get; set; }
        
        public decimal? initialPayment { get; set; }

        public decimal? totalPayment { get; set; }

        public Room Room { get; set; }
    }
}
