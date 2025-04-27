import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomePageConstants } from '../../helpers/Constants';
import { NotesContainerComponent } from '../notescontainer/notescontainer.component';

/**
 * The Home component.
 */
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, NotesContainerComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
class HomeComponent {
  /**
   * The home page title constant.
   */
  homePageTitle: string = HomePageConstants.Headings.WelcomeMessage;

  /**
   * The home page subtitle constant.
   */
  homePageSubTitle: string = HomePageConstants.Headings.SubHeadingMessage;
}

export { HomeComponent };
