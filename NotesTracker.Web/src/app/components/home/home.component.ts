import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomePageConstants } from '../../helpers/notestracker.constants';
import { NotesContainerComponent } from '../notescontainer/notescontainer.component';

/**
 * The Home component.
 */
@Component( {
  selector: 'app-home',
  standalone: true,
  imports: [ CommonModule, NotesContainerComponent ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
} )
class HomeComponent
{
  /**
   * The home page title constant.
   */
  public homePageTitle: string = HomePageConstants.Headings.WelcomeMessage;

  /**
   * The home page subtitle constant.
   */
  public homePageSubTitle: string =
    HomePageConstants.Headings.SubHeadingMessage;
}

export { HomeComponent };
