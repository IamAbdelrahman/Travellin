export interface IPropertyResponse {
  id: string;
  title: string;
  description: string;
  ownerId: string;
  propertyTypeId: number;
  locationId: number;
  pricePerNight: number;
  latitude: number;
  longitude: number;
  safteyInfo: string;
  houseRules: string;
  cancellationPolicy: string;
  isActive: boolean;
  isDeleted: boolean;
}
