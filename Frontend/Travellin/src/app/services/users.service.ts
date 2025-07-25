import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { UserProfiles } from '../models/api/response/iget-users';
import { Observable } from 'rxjs';
import { ApiConstant } from '../utils/api-constant.util';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  constructor(private http: HttpClient) {}
  getUsers(): Observable<HttpResponse<UserProfiles>> {
    return this.http.get<UserProfiles>(ApiConstant.user.getAllUsers, {
      observe: 'response',
      withCredentials: true,
    });
  }
}
