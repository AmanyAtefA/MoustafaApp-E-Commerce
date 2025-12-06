import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HeaderOfferComponent } from './header-offer.component';

describe('HeaderOfferComponent', () => {
  let component: HeaderOfferComponent;
  let fixture: ComponentFixture<HeaderOfferComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HeaderOfferComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HeaderOfferComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
