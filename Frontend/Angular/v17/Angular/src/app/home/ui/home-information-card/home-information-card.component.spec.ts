import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeInformationCardComponent } from './home-information-card.component';

describe('HomeInformationCardComponent', () => {
  let component: HomeInformationCardComponent;
  let fixture: ComponentFixture<HomeInformationCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HomeInformationCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HomeInformationCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
