export interface PropertyDetails {
  id: string;
  title: string;
  description: string;
  safteyInfo: string;
  houseRules: string;
  cancellationPolicy: string;
  maxGuestCount: number;
  spaceSummaries: {
    spaceType: {
      id: number;
      name: string;
    };
    count: number;
    isShared: boolean;
  }[];
  spaceItemSummaries: {
    itemType: {
      id: number;
      name: string;
      propertySpaceTypeId: number;
    };
    quantity: number;
  }[];
  pricePerNight: number;
  latitude: number;
  longitude: number;
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
  photos: {
    id: string;
    photoUrl: string;
  }[];
  averageRating: number;
  reviewCount: number;
  isActive: boolean;
  isDeleted: boolean;
}
