import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomepageComponent } from './containers/homepage/homepage.component';

const routes: Routes = [
  {
    path: 'home',
    component: HomepageComponent
    //canActivate: [ProtectedPageGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  declarations: []
})
export class HomeRoutingModule { }
