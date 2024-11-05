import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GameFormComponent } from './features/games/components/game-form/game-form.component';
import { GameListComponent } from './features/games/components/game-list/game-list.component';
import { AppComponent } from './app.component';

export const routes: Routes = [
  { path: 'game-form', component: GameFormComponent, pathMatch: 'full' },
  { path: 'game-list', component: GameListComponent },
  { path: '', redirectTo: 'game-list', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
``;
