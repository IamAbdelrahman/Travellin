export interface ChatUser {
  id: string;
  userName: string;
  firstName?: string;
  lastName?: string;
  email: string;
  profilePicture?: string;
  isOnline?: boolean;
  lastSeen?: Date;
}