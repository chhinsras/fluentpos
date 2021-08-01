import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerSkeletonComponent } from './customer-skeleton.component';

describe('CustomerSkeletonComponent', () => {
  let component: CustomerSkeletonComponent;
  let fixture: ComponentFixture<CustomerSkeletonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CustomerSkeletonComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerSkeletonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
