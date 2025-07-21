namespace Travellin.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IRegionRepository RegionRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public IPropertyRepository PropertyRepository { get; }
        public IPropertyTypeRepository PropertyTypeRepository { get; }
        public IAmenityRepository AmenityRepository { get; }
        public IAmenityCategoryRepository AmenityCategoryRepository { get; }
        public IGuestTypeReposiotry GuestTypeReposiotry { get; }
        public IPropertyAmenityRepository PropertyAmenityRepository { get; }
        public IPropertyGuestRepository PropertyGuestRepository { get; }
        public IPropertySpaceTypeRepository PropertySpaceTypeRepository { get; }
        public IPropertySpaceItemTypeRepository PropertySpaceItemTypeRepository { get; }
        public IPropertyFeeRepository PropertyFeeRepository { get; }
        public IPropertyAvailabilityRepository PropertyAvailabilityRepository { get; }
        public IPropertySpaceRepository PropertySpaceRepository { get; }
        public IPropertySpaceItemRepository PropertySpaceItemRepository { get; }
        public ILocationRepository LocationRepository { get; }
        public IFavoritePropertyRepository FavoritePropertyRepository { get; }
        public IFileUploadRepository FileUploadRepository { get; }
        public IUserProfileRepository UserProfileRepository { get; }
        public IBookingRepository BookingRepository { get; }
        public IHostUpgradeRequestRepository HostUpgradeRequestRepository { get; }
        public IPropertyPhotoRepository PropertyPhotoRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public IReviewRepository ReviewRepository { get; }
        public Task SaveChangesAsync();
        public void SaveChanges();
    }
}
