export interface ChatUser {
  id: string;
  userName: string;
  email?: string;
  firstName?: string;
  lastName?: string;
  isOnline?: boolean;
}