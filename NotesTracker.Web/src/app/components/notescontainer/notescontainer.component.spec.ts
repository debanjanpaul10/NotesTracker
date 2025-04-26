import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NotescontainerComponent } from './notescontainer.component';

describe('NotescontainerComponent', () => {
  let component: NotescontainerComponent;
  let fixture: ComponentFixture<NotescontainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NotescontainerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NotescontainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
