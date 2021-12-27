import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FirmContactComponent } from './firm-contact.component';

describe('FirmContactComponent', () => {
  let component: FirmContactComponent;
  let fixture: ComponentFixture<FirmContactComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FirmContactComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FirmContactComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
