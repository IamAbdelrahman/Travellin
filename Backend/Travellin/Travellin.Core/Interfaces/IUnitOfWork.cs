
namespace Travellin.Travellin.Core.Interfaces
{
    /// <summary>
    /// The IUnitOfWork interface will define the contract 
    /// for managing database transactions and repository access
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        // Declare properties for each specific repository interface
        IAdminRepository Admins { get; }
        IAmenityCategoryRepository AmenityCategories { get; }
        IAmenityRepository Amenities { get; }
        IBookingGuestRepository BookingGuests { get; }
        IBookingRepository Bookings { get; }
        ICancellationPolicyRepository CancellationPolicies { get; }
        ICoHostAssignmentRepository CoHostAssignments { get; }
        IConversationRepository Conversations { get; }
        ICountryRepository Countries { get; }
        IFavouriteRepository Favourites { get; }
        IHostVerificationRepository HostVerifications { get; }
        // IIdentityFactory IdentityFactory { get; } // Consider if this belongs here or as a separate service
        ILocationRepository Locations { get; }
        IMessageRepository Messages { get; }
        INotificationRepository Notifications { get; }
        IPaymentRepository Payments { get; }
        IPromotionRepository Promotions { get; }
        IPropertyAmenityRepository PropertyAmenities { get; }
        IPropertyAvailabilityRepository PropertyAvailabilities { get; }
        IPropertyFeeRepository PropertyFees { get; }
        IPropertyImageRepository PropertyImages { get; }
        IPropertyRepository Properties { get; }
        IPropertyTypeRepository PropertyTypes { get; }
        IRegionRepository Regions { get; }
        IReportRepository Reports { get; }
        IReviewRepository Reviews { get; }
        // IServiceFactory ServiceFactory { get; } // Consider if this belongs here or as a separate service
        IUserRepository Users { get; }
        IUserUsedPromotionRepository UserUsedPromotions { get; }
        IViolationRepository Violations { get; }

        /// <summary>
        /// Saves all changes made in this unit of work to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        int Complete();
        Task<int> CompleteAsync();
    }
}
