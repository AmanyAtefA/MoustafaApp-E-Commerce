import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DressStyleComponent } from './dress-style.component';

describe('DressStyleComponent', () => {
  let component: DressStyleComponent;
  let fixture: ComponentFixture<DressStyleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DressStyleComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DressStyleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
