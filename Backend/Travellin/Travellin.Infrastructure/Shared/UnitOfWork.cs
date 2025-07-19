using Microsoft.EntityFrameworkCore.Storage;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;
using Travellin.Travellin.Infrastructure.Repositories;

namespace Travellin.Travellin.Infrastructure.Shared
{
    /// <summary>
    /// It will implement this interface, leveraging (EF Core) 
    /// for database operations
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AirbnbDbContext _context;

        // Private fields for lazy loading repositories
        private IAdminRepository _admins;
        private IAmenityCategoryRepository _amenityCategories;
        private IAmenityRepository _amenities;
        private IBookingGuestRepository _bookingGuests;
        private IBookingRepository _bookings;
        private ICancellationPolicyRepository _cancellationPolicies;
        private ICoHostAssignmentRepository _coHostAssignments;
        private IConversationRepository _conversations;
        private ICountryRepository _countries;
        private IFavouriteRepository _favourites;
        private IHostVerificationRepository _hostVerifications;
        private ILocationRepository _locations;
        private IMessageRepository _messages;
        private INotificationRepository _notifications;
        private IPaymentRepository _payments;
        private IPromotionRepository _promotions;
        private IPropertyAmenityRepository _propertyAmenities;
        private IPropertyAvailabilityRepository _propertyAvailabilities;
        private IPropertyFeeRepository _propertyFees;
        private IPropertyImageRepository _propertyImages;
        private IPropertyRepository _properties;
        private IPropertyTypeRepository _propertyTypes;
        private IRegionRepository _regions;
        private IReportRepository _reports;
        private IReviewRepository _reviews;
        private IUserRepository _users;
        private IUserUsedPromotionRepository _userUsedPromotions;
        private IViolationRepository _violations;

        public UnitOfWork(AirbnbDbContext context) 
        {
            _context = context;
        }

        // Public properties with lazy initialization
        public IAdminRepository Admins => _admins ??= new AdminRepository(_context);
        public IAmenityCategoryRepository AmenityCategories => _amenityCategories ??= new AmenityCategoryRepository(_context);
        public IAmenityRepository Amenities => _amenities ??= new AmenityRepository(_context);
        public IBookingGuestRepository BookingGuests => _bookingGuests ??= new BookingGuestRepository(_context);
        public IBookingRepository Bookings => _bookings ??= new BookingRepository(_context);
        public ICancellationPolicyRepository CancellationPolicies => _cancellationPolicies ??= new CancellationPolicyRepository(_context);
        public ICoHostAssignmentRepository CoHostAssignments => _coHostAssignments ??= new CoHostAssignmentRepository(_context);
        public IConversationRepository Conversations => _conversations ??= new ConversationRepository(_context);
        public ICountryRepository Countries => _countries ??= new CountryRepository(_context);
        public IFavouriteRepository Favourites => _favourites ??= new FavouriteRepository(_context);
        public IHostVerificationRepository HostVerifications => _hostVerifications ??= new HostVerificationRepository(_context);
        public ILocationRepository Locations => _locations ??= new LocationRepository(_context);
        public IMessageRepository Messages => _messages ??= new MessageRepository(_context);
        public INotificationRepository Notifications => _notifications ??= new NotificationRepository(_context);
        public IPaymentRepository Payments => _payments ??= new PaymentRepository(_context);
        public IPromotionRepository Promotions => _promotions ??= new PromotionRepository(_context);
        public IPropertyAmenityRepository PropertyAmenities => _propertyAmenities ??= new PropertyAmenityRepository(_context);
        public IPropertyAvailabilityRepository PropertyAvailabilities => _propertyAvailabilities ??= new PropertyAvailabilityRepository(_context);
        public IPropertyFeeRepository PropertyFees => _propertyFees ??= new PropertyFeeRepository(_context);
        public IPropertyImageRepository PropertyImages => _propertyImages ??= new PropertyImageRepository(_context);
        public IPropertyRepository Properties => _properties ??= new PropertyRepository(_context);
        public IPropertyTypeRepository PropertyTypes => _propertyTypes ??= new PropertyTypeRepository(_context);
        public IRegionRepository Regions => _regions ??= new RegionRepository(_context);
        public IReportRepository Reports => _reports ??= new ReportRepository(_context);
        public IReviewRepository Reviews => _reviews ??= new ReviewRepository(_context);
        public IUserRepository Users => _users ??= new UserRepository(_context);
        public IUserUsedPromotionRepository UserUsedPromotions => _userUsedPromotions ??= new UserUsedPromotionRepository(_context);
        public IViolationRepository Violations => _violations ??= new ViolationRepository(_context);


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
