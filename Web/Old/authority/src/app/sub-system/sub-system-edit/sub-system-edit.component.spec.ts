import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubSystemEditComponent } from './sub-system-edit.component';

describe('SubSystemEditComponent', () => {
  let component: SubSystemEditComponent;
  let fixture: ComponentFixture<SubSystemEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubSystemEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubSystemEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
