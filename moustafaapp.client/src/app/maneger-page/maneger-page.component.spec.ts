import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManegerPageComponent } from './maneger-page.component';

describe('ManegerPageComponent', () => {
  let component: ManegerPageComponent;
  let fixture: ComponentFixture<ManegerPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ManegerPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManegerPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
