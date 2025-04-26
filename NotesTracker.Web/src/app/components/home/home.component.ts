import { Component, OnInit } from '@angular/core';
import { Notes } from '../../models/notes.model';
import { CommonModule } from '@angular/common';
import { HomePageConstants } from '../../helpers/Constants';
import { NotesContainerComponent } from '../notescontainer/notescontainer.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, NotesContainerComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {
  notes: Notes[] = [];
  homePageTitle: string = HomePageConstants.Headings.WelcomeMessage;
  homePageSubTitle: string = HomePageConstants.Headings.SubHeadingMessage;
}
