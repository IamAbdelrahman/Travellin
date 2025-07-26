import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpgradeTohostComponent } from './upgrade-tohost.component';

describe('UpgradeTohostComponent', () => {
  let component: UpgradeTohostComponent;
  let fixture: ComponentFixture<UpgradeTohostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpgradeTohostComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(UpgradeTohostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
