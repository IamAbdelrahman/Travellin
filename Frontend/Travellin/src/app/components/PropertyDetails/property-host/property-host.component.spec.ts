import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertyHostComponent } from './property-host.component';

describe('PropertyHostComponent', () => {
  let component: PropertyHostComponent;
  let fixture: ComponentFixture<PropertyHostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PropertyHostComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(PropertyHostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
