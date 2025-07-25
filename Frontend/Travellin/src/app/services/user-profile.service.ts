import {
  HttpClient,
  HttpErrorResponse,
  HttpEvent,
  HttpEventType,
  HttpHeaders,
  HttpResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, throwError } from 'rxjs';
import { IUserProfile } from '../models/domain/iuser-profile';
import { ApiConstant } from '../utils/api-constant.util';
import { ApiUserProfileRequest } from '../models/api/request/api-user-profile-request';

@Injectable({
  providedIn: 'root',
})
export class UserProfileService {
  constructor(private http: HttpClient) {}

  getUserProfile(): Observable<any> {
    return this.http.get<IUserProfile>(ApiConstant.UserProfile.User, {
      observe: 'response',

      withCredentials: true,
    });
  }

  //   UpdateUserProfile(dto: any): Observable<any> {
  //       return this.http.patch<IUserProfile>(ApiConstant.UserProfile.User, dto, {
  //         observe: 'response',
  //         withCredentials: true,
  //       })
  // }

  // user-profile.service.ts
  // UpdateUserProfile(dto: any | FormData): Observable<IUserProfile> {
  //   const isFormData = dto instanceof FormData;
  //   const options = {
  //     observe: 'response' as const,
  //     withCredentials: true,
  //     // Add headers based on the data type
  //     headers: isFormData ? {} : { 'Content-Type': 'application/json' }
  //   };

  //   // If it's not FormData, create a new FormData object
  //   let requestData: FormData;
  //   if (!isFormData) {
  //     requestData = new FormData();
  //     // Convert the DTO to FormData
  //     Object.keys(dto).forEach(key => {
  //       const value = dto[key];
  //       if (value !== null && value !== undefined) {
  //         if (typeof value === 'object') {
  //           requestData.append(key, JSON.stringify(value));
  //         } else {
  //           requestData.append(key, value);
  //         }
  //       }
  //     });
  //   } else {
  //     requestData = dto as FormData;
  //   }

  //   return this.http.patch<any>(
  //     ApiConstant.UserProfile.User,
  //     requestData
  //   ).pipe(
  //     map(response => this.normalizeProfileResponse(response.body ?? undefined)),
  //     catchError(this.handleError)
  //   );
  // }

  // private normalizeProfileResponse(response?: ApiUserProfileRequest): IUserProfile {
  //   if (!response) {
  //     throw new Error('Empty response from server');
  //   }

  //   return {
  //     userId: response.userId ?? '',
  //     userName: response.userName ?? '',
  //     email: response.email ?? '',
  //     roles: response.roles ?? [],
  //     firstName: response.firstName ?? '',
  //     lastName: response.lastName ?? '',
  //     phoneNumber: response.phoneNumber ?? '',
  //     bio: response.bio ?? '',
  //     birthDate:'2025-04-26',
  //     country: response.country ?? { id: 0, name: '', regionId: 0 },
  //     photo: response.photo ?? { id: '', photoUrl: '' }
  //   };
  // }
  private handleError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = 'An unknown error occurred';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Error: ${error.error.message}`;
    } else {
      errorMessage = error.error?.message || error.message;
    }
    return throwError(() => new Error(errorMessage));
  }

  UpdateUserProfile(dto: any | FormData): Observable<IUserProfile> {
    // Get the access token from localStorage
    const accessToken = localStorage.getItem('accessToken');

    // Create headers object
    const headers: { [key: string]: string } = {};

    // Add Authorization header if token exists
    if (accessToken) {
      headers['Authorization'] = `Bearer ${accessToken}`;
      // headers['Content-Type'] = 'application/json';
    }

    const isFormData = dto instanceof FormData;

    // Set Content-Type only for non-FormData requests
    // if (!isFormData) {
    //   headers['Content-Type'] = 'application/json';
    // }

    // Prepare request options
    const options = {
      observe: 'response' as const,
      withCredentials: true,

      headers: new HttpHeaders(headers),
    };

    // Process the request data
    let requestData: FormData | string;
    // if (!isFormData) {
    //   requestData = dto;
    // } else {
    //   requestData = dto;
    // }

    requestData = dto;

    return this.http
      .patch<any>(ApiConstant.UserProfile.User, requestData, options)
      .pipe(
        map(response =>
          this.normalizeProfileResponse(response.body ?? undefined)
        ),
        catchError(this.handleError)
      );
  }

  private normalizeProfileResponse(
    response?: ApiUserProfileRequest
  ): IUserProfile {
    if (!response) {
      throw new Error('Empty response from server');
    }

    return {
      userId: response.userId ?? '',
      userName: response.userName ?? '',
      email: response.email ?? '',
      roles: response.roles ?? [],
      firstName: response.firstName ?? '',
      lastName: response.lastName ?? '',
      phoneNumber: response.phoneNumber ?? '',
      bio: response.bio ?? '',
      birthDate: response.birthDate
        ? new Date(response.birthDate).toISOString().split('T')[0]
        : '2025-04-26',
      country: response.country ?? { id: 0, name: '', regionId: 0 },
      photo: response.photo ?? { id: '', photoUrl: '' },
    };
  }
}
