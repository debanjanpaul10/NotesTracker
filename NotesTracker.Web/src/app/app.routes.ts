import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { HomeComponent } from './components/home/home.component';
import { AddNoteComponent } from './components/addnote/addnote.component';
import { ErrorPageComponent } from './components/common/error-page/error-page.component';
import { AngularRoutes } from './helpers/notestracker.constants';

/**
 * The configured routes.
 */
export const routes: Routes = [
  { path: AngularRoutes.Home.Name, component: HomeComponent },
  {
    path: AngularRoutes.AddNote.Name,
    component: AddNoteComponent,
  },
  { path: AngularRoutes.Error.Name, component: ErrorPageComponent },
];

/**
 * The Notes routing module.
 */
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class NotesRoutingModule {}
