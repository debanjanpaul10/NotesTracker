import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { HomeComponent } from './components/home/home.component';
import { NoteComponent } from './components/note/note.component';
import { AddNoteComponent } from './components/addnote/addnote.component';
import { ErrorPageComponent } from './components/common/error-page/error-page.component';
import { AngularRoutes } from './helpers/Constants';

/**
 * The configured routes.
 */
export const routes: Routes = [
  { path: AngularRoutes.Home, component: HomeComponent },
  { path: AngularRoutes.Note, component: NoteComponent },
  { path: AngularRoutes.AddNote, component: AddNoteComponent },
  { path: AngularRoutes.Error, component: ErrorPageComponent },
];

/**
 * The Notes routing module.
 */
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class NotesRoutingModule {}
