namespace Travellin.Core.Dtos.PropertyFees
{
    public class PropertyFeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string PropertyId { get; set; }
    }
}
