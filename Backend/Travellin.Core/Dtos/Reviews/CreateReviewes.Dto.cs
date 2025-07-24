namespace Travellin.Core.Dtos.Reviews
{
    public class CreateReviewDto
    {
        public string BookingId { get; set; }
        public string Comment { get; set; }
        public decimal Cleanliness { get; set; }
        public decimal Accuracy { get; set; }
        public decimal CheckIn { get; set; }
        public decimal Communication { get; set; }
        public decimal Location { get; set; }
        public decimal Value { get; set; }
    }
}