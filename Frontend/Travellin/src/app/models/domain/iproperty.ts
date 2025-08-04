export interface IProperty {
  id: string;
  title: string;
  description: string;
  safteyInfo: string;
  houseRules: string;
  pricePerNight: number;
  isactive: boolean;
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
  cancellationpolicy: string
  isInstant: boolean;
}
