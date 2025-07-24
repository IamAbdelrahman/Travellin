import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Property } from '../models/property.model';
import {ApiConstant} from '../../../helpers/api-constant.helper';

@Injectable({
    providedIn: 'root'
})
export class PropertiesService {
    constructor(private http: HttpClient) {}

    getProperties(): Observable<Property[]> {
        return this.http.get<Property[]>(ApiConstant.PropertiesApi.getAll);
    }
    
    getPropertyById(id: string): Observable<Property> {
        return this.http.get<Property>(ApiConstant.PropertiesApi.getById.replace('{id}', id));
    }

    smartSearch(query: string): Observable<Property[]> {
        return this.http.get<Property[]>(`${ApiConstant.PropertiesApi.smartSearch}?query=${encodeURIComponent(query)}`);
    }

    getAllPropertyTypes(): Observable<any[]> {
        return this.http.get<any[]>(ApiConstant.PropertiesApi.getAllPropertyTypes);
    }

    getPropertyAmenities(id: string): Observable<any[]> {
        return this.http.get<any[]>(ApiConstant.PropertiesApi.getPropertyAmenities.replace('{id}', id));
    }

    getPropertyAvailability(id: string): Observable<any[]> {
        return this.http.get<any[]>(ApiConstant.PropertiesApi.getPropertyAvailability.replace('{id}', id));
    }
}