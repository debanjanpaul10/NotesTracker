import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { AngularRoutes } from './helpers/notestracker.constants';

/**
 * The configured routes.
 */
export const routes: Routes = [
  {
    path: AngularRoutes.Home.Name,
    loadComponent: () =>
      import('./components/home/home.component').then((c) => c.HomeComponent),
  },
  {
    path: AngularRoutes.AddNote.Name,
    loadComponent: () =>
      import('./components/addnote/addnote.component').then(
        (c) => c.AddNoteComponent
      ),
  },
  {
    path: AngularRoutes.Error.Name,
    loadComponent: () =>
      import('./components/common/error-page/error-page.component').then(
        (c) => c.ErrorPageComponent
      ),
  },
];

/**
 * The Notes routing module.
 */
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class NotesRoutingModule {}
