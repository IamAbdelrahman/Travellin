export interface User {
  userId: string;
  userName: string;
  email: string;
  roles: string[];
  firstName: string;
  lastName: string;
  phoneNumber: string;
  bio: string;
  birthDate: string;
  country: {
    id: number;
    name: string;
    regionId: number;
  };
  photo: {
    id: string;
    photoUrl: string;
  };
}

export interface UserProfiles {
  items: User[];
  metaData: {
    page: number;
    pageSize: number;
    total: number;
  };
}
