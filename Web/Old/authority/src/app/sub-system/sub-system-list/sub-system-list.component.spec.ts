import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubSystemListComponent } from './sub-system-list.component';

describe('SubSystemListComponent', () => {
  let component: SubSystemListComponent;
  let fixture: ComponentFixture<SubSystemListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubSystemListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubSystemListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
