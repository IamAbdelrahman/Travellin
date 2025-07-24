import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HostDashboard } from './host-dashboard';

describe('HostDashboard', () => {
  let component: HostDashboard;
  let fixture: ComponentFixture<HostDashboard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HostDashboard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HostDashboard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
