
namespace Travellin.Travellin.Core.Interfaces
{
    /// <summary>
    /// The IUnitOfWork interface will define the contract 
    /// for managing database transactions and repository access
    /// </summary>
    public interface IUnitOfWork
    {
        // Repository properties for entity access
        IUserRepository Users { get; }
        IPropertyRepository Properties { get; }
        IPropertyTypeRepository PropertyTypes { get; }
        IPropertyImageRepository PropertyImages { get; }
        ICoHostAssignmentRepository CoHostAssignments { get; }
        IViolationRepository Violations { get; }
        IConversationRepository Conversations { get; }
        IMessageRepository Messages { get; }
        IBookingRepository Bookings { get; }
        IBookingGuestRepository BookingGuests { get; }
        IPaymentRepository Payments { get; }
        IReviewRepository Reviews { get; }
        INotificationRepository Notifications { get; }
        IAdminRepository Admins { get; }
        IReportRepository Reports { get; }
        IPromotionRepository Promotions { get; }
        IUserUsedPromotionRepository UserUsedPromotions { get; }
        IPropertyAmenityRepository PropertyAmenities { get; }
        IPropertyAvailabilityRepository PropertyAvailabilities { get; }
        IPropertyFeeRepository PropertyFees { get; }

        // Save changes method
        Task SaveChangesAsync();
    }
}
