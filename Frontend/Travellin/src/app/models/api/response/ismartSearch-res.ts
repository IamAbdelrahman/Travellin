import { IProperty } from '../../domain/iproperty';

export interface ISmartSearchRes {
  searchParams: {
    locationId: number | null;
    locationName: string | null;
    propertyTypeId: number | null;
    guestCount: number | null;
    priceMin: number | null;
    priceMax: number | null;
    minAvgRating: number | null;
    maxAvgRating: number | null;
    checkIn: string | null;
    checkOut: string | null;
    countryId: number | null;
    countryName: string | null;
    regionId: number;
    ownerId: string | null;
    isActive: boolean | null;
    isDeleted: boolean | null;
    sort: string | null;
    page: number;
    pageSize: number;
  };

  items: IProperty[];

  metaData: {
    page: number;
    pageSize: number;
    total: number;
  };
}
