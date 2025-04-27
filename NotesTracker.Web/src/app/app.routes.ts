import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { HomeComponent } from './components/home/home.component';
import { NoteComponent } from './components/note/note.component';
import { AddNoteComponent } from './components/addnote/addnote.component';

/**
 * The configured routes.
 */
export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'notes/:noteId', component: NoteComponent },
  { path: 'addnote', component: AddNoteComponent },
];

/**
 * The Notes routing module.
 */
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class NotesRoutingModule {}
