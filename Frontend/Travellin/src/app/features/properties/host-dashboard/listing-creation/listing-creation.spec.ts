import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListingCreation } from './listing-creation';

describe('ListingCreation', () => {
  let component: ListingCreation;
  let fixture: ComponentFixture<ListingCreation>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ListingCreation]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListingCreation);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
