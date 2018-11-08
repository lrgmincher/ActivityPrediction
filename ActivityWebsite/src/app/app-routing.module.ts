import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MappageComponent } from './mappage/mappage.component';

const routes: Routes = [
  { path: 'app-mappage', component: MappageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
