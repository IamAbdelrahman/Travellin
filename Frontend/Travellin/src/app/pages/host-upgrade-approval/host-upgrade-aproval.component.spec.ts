import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HostUpgradeAprovalComponent } from './host-upgrade-aproval.component';

describe('HostUpgradeAprovalComponent', () => {
  let component: HostUpgradeAprovalComponent;
  let fixture: ComponentFixture<HostUpgradeAprovalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HostUpgradeAprovalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(HostUpgradeAprovalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
