export interface IPropertyRequest {
  id?: string;
  title: string;
  description: string;
  propertyTypeId: number;
  locationId: number;
  pricePerNight: number;
  latitude: number;
  longitude: number;
  safteyInfo: string;
  houseRules: string;
  cancellationPolicy: string;
}
