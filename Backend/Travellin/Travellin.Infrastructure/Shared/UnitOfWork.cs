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
        private readonly AirbnbDbContext _dbContext;

        // Repository instances
        private IUserRepository? _users;
        private IPropertyRepository? _properties;
        private IPropertyTypeRepository? _propertyTypes;
        private IPropertyImageRepository? _propertyImages;
        private ICoHostAssignmentRepository? _coHostAssignments;
        private IViolationRepository? _violations;
        private IConversationRepository? _conversations;
        private IMessageRepository? _messages;
        private IBookingRepository? _bookings;
        private IBookingGuestRepository? _bookingGuests;
        private IPaymentRepository? _payments;
        private IReviewRepository? _reviews;
        private INotificationRepository? _notifications;
        private IUserUsedPromotionRepository? _userUsedPromotions;
        private IPropertyAmenityRepository? _propertyAmenities;
        private IPropertyAvailabilityRepository? _propertyAvailabilities;
        private IPropertyFeeRepository? _propertyFees;

        public UnitOfWork(AirbnbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUserRepository Users => _users ??= new UserRepository(_dbContext);
        public IPropertyRepository Properties => _properties ??= new PropertyRepository(_dbContext);
        public IPropertyTypeRepository PropertyTypes => _propertyTypes ??= new PropertyTypeRepository(_dbContext);
        public IPropertyImageRepository PropertyImages => _propertyImages ??= new PropertyImageRepository(_dbContext);
        public ICoHostAssignmentRepository CoHostAssignments => _coHostAssignments ??= new CoHostAssignmentRepository(_dbContext);
        public IViolationRepository Violations => _violations ??= new ViolationRepository(_dbContext);
        public IConversationRepository Conversations => _conversations ??= new ConversationRepository(_dbContext);
        public IMessageRepository Messages => _messages ??= new MessageRepository(_dbContext);
        public IBookingRepository Bookings => _bookings ??= new BookingRepository(_dbContext);
        public IBookingGuestRepository BookingGuests => _bookingGuests ??= new BookingGuestRepository(_dbContext);
        public IPaymentRepository Payments => _payments ??= new PaymentRepository(_dbContext);
        public IReviewRepository Reviews => _reviews ??= new ReviewRepository(_dbContext);
        public INotificationRepository Notifications => _notifications ??= new NotificationRepository(_dbContext);
        public IUserUsedPromotionRepository UserUsedPromotions => _userUsedPromotions ??= new UserUsedPromotionRepository(_dbContext);
        public IPropertyAmenityRepository PropertyAmenities => _propertyAmenities ??= new PropertyAmenityRepository(_dbContext);
        public IPropertyAvailabilityRepository PropertyAvailabilities => _propertyAvailabilities ??= new PropertyAvailabilityRepository(_dbContext);
        public IPropertyFeeRepository PropertyFees => _propertyFees ??= new PropertyFeeRepository(_dbContext);

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
