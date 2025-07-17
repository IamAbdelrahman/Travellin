namespace Travellin.Travellin.Core.Enums
{
    public enum AvailabilityStatus
    {
        Available = 0,      // Default - not blocked or booked
        Booked = 1,         // Reserved by a guest
        HostBlocked = 2,    // Blocked manually by the host
        Maintenance = 3,    // Temporarily unavailable due to repairs
        UnderReview = 4,    // Possibly blocked by system for review/fraud
        SeasonalClosure = 5 // Closed during off-season by host
    }

}
