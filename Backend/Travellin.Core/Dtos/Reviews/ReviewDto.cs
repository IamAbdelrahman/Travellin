namespace Travellin.Core.Dtos.Reviews
{
    public class Reviewer
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
    }
    public class ReviewDto
    {
        public string Id { get; set; }
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
        public Reviewer Reviewer { get; set; }
        public decimal Avg => (Cleanliness + Accuracy + CheckIn + Communication + Location + Value) / 6;
    }
}