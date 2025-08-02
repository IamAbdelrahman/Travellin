import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse,HttpHeaders  } from '@angular/common/http';
import { UserProfiles } from '../models/api/response/iget-users';
import { map, Observable } from 'rxjs';
import { ApiConstant } from '../utils/api-constant.util';
import { ChatUser } from '../models/chat/user.model';
import { TokenStorageService } from './token-storage.service';
import { environment } from '../../environments/environment';
@Injectable({
  providedIn: 'root',
})
export class UsersService {
  constructor(private http: HttpClient,
    private tokenStorage: TokenStorageService) {}
      private getHttpHeaders(): HttpHeaders {
    const token = this.tokenStorage.getAccessToken();
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }
  getUsers(): Observable<HttpResponse<UserProfiles>> {
    return this.http.get<UserProfiles>(ApiConstant.user.getAllUsers, {
      observe: 'response',
      withCredentials: true,
    });
  }
  getAllUsers(): Observable<ChatUser[]> {
    return this.getUsers().pipe(
      map(response => {
        // Transform your existing UserProfiles to ChatUser format
        // Adjust this mapping based on your UserProfiles structure
        const userProfiles = response.body;
        if (!userProfiles || !userProfiles.items) { 
          return [];
        }

        return userProfiles.items.map(user => ({
          id: user.userId,
          userName: user.userName || user.email,
          email: user.email,
          firstName: user.firstName,
          lastName: user.lastName,
          isOnline: false // You can implement online status logic later
        } as ChatUser));
      })
    );
  }
  getUserById(userId: string): Observable<{ email: string }> {
  return this.http.get<{ email: string }>(
    `${environment.apiUrl}/User/${userId}`,
    {
      headers: this.getHttpHeaders()
    }
  );
}
  deleteUser(id: string): Observable<any> {
    const url = `${ApiConstant.UserProfile.Delete}/${id}`;
    return this.http.delete(url, { withCredentials: true });
  }

}
