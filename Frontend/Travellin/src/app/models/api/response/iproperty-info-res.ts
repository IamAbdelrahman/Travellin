export interface IPropertyInfo {
  id: string;
  title: string;
  description: string;
  safteyInfo: string;
  houseRules: string;
  cancellationPolicy: string;
  maxGuestCount: number;
  pricePerNight: number;
  latitude: number;
  longitude: number;
  averageRating: number;
  reviewCount: number;

  owner: {
    id: string;
    userName: string;
  };

  location: {
    id: number;
    name: string;
    countryId: number;
  };

  propertyType: {
    id: number;
    name: string;
    icon: string;
  };

  photos: Array<{
    id: string;
    photoUrl: string;
  }>;

  spaceSummaries: Array<{
    spaceType: {
      id: number;
      name: string;
    };
    count: number;
    isShared: boolean;
  }>;

  spaceItemSummaries: Array<{
    itemType: {
      id: number;
      name: string;
      propertySpaceTypeId: number;
    };
    quantity: number;
  }>;
}
