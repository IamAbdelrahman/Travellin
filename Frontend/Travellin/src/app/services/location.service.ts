import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { ApiConstant } from '../utils/api-constant.util';

export interface Country {
  id: number;
  name: string;
  regionId: number;
}

export interface Location {
  id: number;
  name: string;
  countryId: number;
}

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  private countriesSubject = new BehaviorSubject<Country[]>([]);
  private locationsSubject = new BehaviorSubject<Location[]>([]);
  
  public countries$ = this.countriesSubject.asObservable();
  public locations$ = this.locationsSubject.asObservable();

  constructor(private http: HttpClient) {
    this.loadCountries();
  }

  // Load all countries
  loadCountries(): Observable<Country[]> {
    return this.http.get<Country[]>(ApiConstant.country.getAllCountries, {
      withCredentials: true
    });
  }

  // Load locations for a specific country
  loadLocations(countryId: number): Observable<Location[]> {
    return this.http.get<Location[]>(`${ApiConstant.location.getAllLocations}?countryId=${countryId}`, {
      withCredentials: true
    });
  }

  // Get countries with caching
  getCountries(): Observable<Country[]> {
    const currentCountries = this.countriesSubject.value;
    if (currentCountries.length > 0) {
      return this.countries$;
    }
    
    this.loadCountries().subscribe({
      next: (countries) => {
        this.countriesSubject.next(countries);
      },
      error: (error) => {
        console.error('Error loading countries:', error);
      }
    });
    
    return this.countries$;
  }

  // Get locations for a country with caching
  getLocations(countryId: number): Observable<Location[]> {
    this.loadLocations(countryId).subscribe({
      next: (locations) => {
        this.locationsSubject.next(locations);
      },
      error: (error) => {
        console.error('Error loading locations:', error);
      }
    });
    
    return this.locations$;
  }

  // Filter countries by search term
  filterCountries(searchTerm: string): Country[] {
    const countries = this.countriesSubject.value;
    if (!searchTerm.trim()) {
      return countries;
    }
    
    return countries.filter(country =>
      country.name.toLowerCase().includes(searchTerm.toLowerCase())
    );
  }

  // Filter locations by search term
  filterLocations(searchTerm: string): Location[] {
    const locations = this.locationsSubject.value;
    if (!searchTerm.trim()) {
      return locations;
    }
    
    return locations.filter(location =>
      location.name.toLowerCase().includes(searchTerm.toLowerCase())
    );
  }

  // Clear cache
  clearCache(): void {
    this.countriesSubject.next([]);
    this.locationsSubject.next([]);
  }
} 