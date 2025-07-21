using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Data;
using Travellin.Infrastructure.Repositories;

namespace Travellin.Infrastructure.Shared
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TravellinDbContext _dbContext;
        private IRegionRepository? _regionRepository;
        private ICountryRepository? _countryRepository;
        private IPropertyRepository? _propertyRepository;
        private IPropertyTypeRepository? _propertyTypeRepository;
        private IAmenityRepository? _amenityRepository;
        private IAmenityCategoryRepository? _amenityCategoryRepository;
        private IGuestTypeReposiotry? _guestTypeReposiotry;
        private IPropertyAmenityRepository? _propertyAmenityRepository;
        private IPropertyGuestRepository? _propertyGuestRepository;
        private IPropertySpaceTypeRepository? _propertySpaceTypeRepository;
        private IPropertySpaceItemTypeRepository _propertySpaceItemTypeRepository;
        private IPropertyFeeRepository _propertyFeeRepository;
        private IPropertyAvailabilityRepository _propertyAvailabilityRepository;
        private IPropertySpaceRepository _propertySpaceRepository;
        private IPropertySpaceItemRepository _propertySpaceItemRepository;
        private ILocationRepository _locationRepository;
        private IFavoritePropertyRepository? _favoritePropertyRepository;
        private IUserProfileRepository? _userProfileRepository;
        private IFileUploadRepository? _fileUploadRepository;
        private IBookingRepository? _bookingRepository;
        private IHostUpgradeRequestRepository? _hostUpgradeRequestRepository;
        private IPropertyPhotoRepository? _propertyPhotoRepository;
        private IPaymentRepository? _paymentRepository;
        private IReviewRepository? _reviewRepository;

        public UnitOfWork(TravellinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRegionRepository RegionRepository => _regionRepository ??= new RegionRepository(_dbContext);
        public ICountryRepository CountryRepository => _countryRepository ??= new CountryRepository(_dbContext);
        public IPropertyRepository PropertyRepository => _propertyRepository ??= new PropertyRepository(_dbContext);
        public IPropertyTypeRepository PropertyTypeRepository => _propertyTypeRepository ??= new PropertyTypeRepository(_dbContext);
        public IAmenityRepository AmenityRepository => _amenityRepository ??= new AmenityRepository(_dbContext);
        public IAmenityCategoryRepository AmenityCategoryRepository => _amenityCategoryRepository ??= new AmenityCategoryRepository(_dbContext);
        public IGuestTypeReposiotry GuestTypeReposiotry => _guestTypeReposiotry ??= new GuestTypeRepository(_dbContext);
        public IPropertyAmenityRepository PropertyAmenityRepository => _propertyAmenityRepository ??= new PropertyAmenityRepository(_dbContext);
        public IPropertyGuestRepository PropertyGuestRepository => _propertyGuestRepository ??= new PropertyGuestRepository(_dbContext);
        public IPropertySpaceTypeRepository PropertySpaceTypeRepository => _propertySpaceTypeRepository ??= new PropertySpaceTypeRepository(_dbContext);
        public IPropertySpaceItemTypeRepository PropertySpaceItemTypeRepository => _propertySpaceItemTypeRepository ??= new PropertySpaceItemTypeRepository(_dbContext);
        public IPropertyFeeRepository PropertyFeeRepository => _propertyFeeRepository ??= new PropertyFeeRepository(_dbContext);
        public IPropertyAvailabilityRepository PropertyAvailabilityRepository => _propertyAvailabilityRepository ??= new PropertyAvailabilityRepository(_dbContext);
        public IPropertySpaceRepository PropertySpaceRepository => _propertySpaceRepository ??= new PropertySpaceRepository(_dbContext);
        public IPropertySpaceItemRepository PropertySpaceItemRepository => _propertySpaceItemRepository ??= new PropertySpaceItemRepository(_dbContext);
        public ILocationRepository LocationRepository => _locationRepository = new LocationRepository(_dbContext);
        public IFavoritePropertyRepository FavoritePropertyRepository => _favoritePropertyRepository ??= new FavoritePropertyRepository(_dbContext);
        public IUserProfileRepository UserProfileRepository => _userProfileRepository ??= new UserProfileRepository(_dbContext);
        public IFileUploadRepository FileUploadRepository => _fileUploadRepository ??= new FileUploadRepository(_dbContext);
        public IBookingRepository BookingRepository => _bookingRepository ??= new BookingRepository(_dbContext);
        public IHostUpgradeRequestRepository HostUpgradeRequestRepository => _hostUpgradeRequestRepository ??= new HostUpgradeRequestRepository(_dbContext);
        public IPropertyPhotoRepository PropertyPhotoRepository => _propertyPhotoRepository ??= new PropertyPhotoRepository(_dbContext);
        public IPaymentRepository PaymentRepository =>
            _paymentRepository ??= new PaymentRepository(_dbContext);
        public IReviewRepository ReviewRepository => _reviewRepository ??= new ReviewRepository(_dbContext);

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
