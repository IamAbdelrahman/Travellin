namespace Travellin.Travellin.Core.Enums
{
    public enum ReviewStatus
    {
        Pending,    // Review period is open
        Submitted,   // Review has been submitted
        Published,   // Review is publicly visible
        Hidden,      // Review is hidden (admin action)
        Expired      // Review period has expired
    }
} 