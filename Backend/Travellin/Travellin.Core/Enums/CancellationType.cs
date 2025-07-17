namespace Travellin.Travellin.Core.Enums
{
    public enum CancellationType
    {
        Flexible, 	    // Full refund 1 day prior to check-in.
        Moderate,       // Full refund 5 days prior to check-in.
        Strict,  	    // 50% refund up to 1 week prior to check-in.
        NonRefundable, 	// No refund after booking. Optional partial refund before check-in.
        SuperStrict 	// Full refund only 30+ days before check-in.
    }
}






