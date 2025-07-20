namespace Travellin.Core.Entities
{
    public class Review : BaseEntity<string>
    {
        public string BookingId { get; set; }
        public string Comment { get; set; }
        public decimal Cleanliness { get; set; }
        public decimal Accuracy { get; set; }
        public decimal CheckIn { get; set; }
        public decimal Communication { get; set; }
        public decimal Location { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
