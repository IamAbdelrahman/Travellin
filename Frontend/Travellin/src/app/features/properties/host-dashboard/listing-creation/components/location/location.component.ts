import { Component, ElementRef, ViewChild, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

declare var google: any;

@Component({
  selector: 'app-location',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.css']
})
export class LocationComponent implements AfterViewInit {
  @ViewChild('googleMap') googleMapElement!: ElementRef;

  searchQuery = '';
  locationSuggestions: any[] = [];
  selectedLocation: any = null;
  map: any;
  marker: any;
  autocompleteService: any;
  placesService: any;

  ngAfterViewInit() {
    this.initMap();
  }

  initMap() {
    const mapOptions = {
      center: { lat: 0, lng: 0 },
      zoom: 2,
      styles: [
        {
          featureType: "poi",
          stylers: [{ visibility: "off" }]
        }
      ]
    };
    this.map = new google.maps.Map(this.googleMapElement.nativeElement, mapOptions);
    this.autocompleteService = new google.maps.places.AutocompleteService();
    this.placesService = new google.maps.places.PlacesService(this.map);
  }

  onSearchChange() {
    if (this.searchQuery.length > 2) {
      this.autocompleteService.getPlacePredictions(
        { input: this.searchQuery },
        (predictions: any[], status: string) => {
          if (status === 'OK') {
            this.locationSuggestions = predictions;
          }
        }
      );
    } else {
      this.locationSuggestions = [];
    }
  }

  selectLocation(suggestion: any) {
    this.placesService.getDetails(
      { placeId: suggestion.place_id },
      (place: any, status: string) => {
        if (status === 'OK') {
          this.selectedLocation = {
            description: place.formatted_address,
            lat: place.geometry.location.lat(),
            lng: place.geometry.location.lng()
          };
          this.updateMap(place.geometry.location);
          this.locationSuggestions = [];
          this.searchQuery = place.formatted_address;
        }
      }
    );
  }

  updateMap(location: any) {
    this.map.setCenter(location);
    this.map.setZoom(15);

    if (this.marker) {
      this.marker.setMap(null);
    }

    this.marker = new google.maps.Marker({
      position: location,
      map: this.map,
      icon: {
        url: 'https://maps.google.com/mapfiles/ms/icons/red-dot.png'
      }
    });
  }

  locateMe() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (position) => {
          const pos = {
            lat: position.coords.latitude,
            lng: position.coords.longitude
          };

          new google.maps.Geocoder().geocode(
            { location: pos },
            (results: any[], status: string) => {
              if (status === 'OK' && results[0]) {
                this.selectedLocation = {
                  description: results[0].formatted_address,
                  lat: pos.lat,
                  lng: pos.lng
                };
                this.searchQuery = results[0].formatted_address;
                this.updateMap(pos);
              }
            }
          );
        },
        () => {
          alert('Unable to retrieve your location');
        }
      );
    } else {
      alert('Geolocation is not supported by this browser');
    }
  }

  searchLocation() {
    if (this.searchQuery.trim()) {
      new google.maps.Geocoder().geocode(
        { address: this.searchQuery },
        (results: any[], status: string) => {
          if (status === 'OK' && results[0]) {
            this.selectedLocation = {
              description: results[0].formatted_address,
              lat: results[0].geometry.location.lat(),
              lng: results[0].geometry.location.lng()
            };
            this.updateMap(results[0].geometry.location);
          } else {
            alert('Location not found');
          }
        }
      );
    }
  }
}
