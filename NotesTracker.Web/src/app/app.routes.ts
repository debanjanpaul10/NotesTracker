import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { HomeComponent } from './components/home/home.component';
import { NoteComponent } from './components/note/note.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'notes/:noteId', component: NoteComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class NotesRoutingModule {}
