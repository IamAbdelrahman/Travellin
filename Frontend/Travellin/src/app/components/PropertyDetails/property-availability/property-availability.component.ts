import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { PropertyService } from '../../../services/property.service';
import { IPropertyInfo } from '../../../models/domain/iproperty-info';
import { CommonModule } from '@angular/common';
import { FullCalendarModule } from '@fullcalendar/angular';
import { CalendarOptions } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';
import { IPropertyAvailability } from '../../../models/domain/iproperty-availability';

@Component({
  selector: 'app-property-availability',
  imports: [CommonModule, FullCalendarModule],
  templateUrl: './property-availability.component.html',
  styleUrl: './property-availability.component.css',
})
export class PropertyAvailabilityComponent implements OnInit {
  @Input() property!: IPropertyInfo;
  loading = true;
  calendarEvents: any[] = [];

  calendarOptions: CalendarOptions = {
    initialView: 'dayGridMonth',
    plugins: [dayGridPlugin, interactionPlugin],
    headerToolbar: {
      left: 'prev,next today',
      center: 'title',
      right: 'dayGridMonth,dayGridWeek,timeGridDay',
    },
    events: [],
    eventDisplay: 'block',
    eventBackgroundColor: '#EF7768',
    eventColor: '#EF7768',
    eventTextColor: '#EF7768',
    eventDidMount: function (info) {
      console.log('Event rendered:', info.event);
    },
    buttonText: {
      today: 'Today',
      month: 'Month',
      week: 'Week',
      day: 'Day',
    },
  };

  constructor(private propertyService: PropertyService) {}

  ngOnInit(): void {
    this.fetchAvailability();
  }

  fetchAvailability(): void {
    this.propertyService
      .getPropertyAvailabilityById(this.property?.id)
      .subscribe({
        next: response => {
          console.log('API Response3:', response);
          this.calendarOptions.events = this.transformBookingsToEvents(
            response.body.items
          );
          if (this.calendarOptions.events.length > 0) {
            this.calendarOptions.initialDate =
              this.calendarOptions.events[0].start;
          }
          console.log('Transformed Events:', this.calendarOptions.events);
          this.calendarOptions = {
            ...this.calendarOptions,
            events: [...this.calendarOptions.events],
          };

          this.loading = false;
        },
        error: err => {
          console.error('Error fetching availability:', err);
          this.loading = false;
        },
      });
  }

  private transformBookingsToEvents(bookings: IPropertyAvailability[]): any[] {
    return bookings.map(booking => ({
      title: 'Available',
      start: booking.startDate,
      end: booking.endDate,
      color: '#EF7768',
      textColor: '#ffffff',
      display: 'auto',
    }));
  }
}
