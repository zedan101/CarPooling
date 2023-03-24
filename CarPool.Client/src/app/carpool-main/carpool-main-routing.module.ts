import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { OfferRideComponent } from './components/offerride/offerride.component';
import { TakeRideComponent } from './components/takeride/takeride.component';
import { MyRidesComponent } from './components/my-rides/my-rides.component';
import { ProfileComponent } from './components/profile/profile.component';

const routes: Routes = [
  
  { path: '', redirectTo: 'home',pathMatch:'full'},
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'offer-ride',
    component: OfferRideComponent
  },
  {
    path: 'take-ride',
    component: TakeRideComponent,
  },
  {
    path: 'my-rides',
    component: MyRidesComponent
  },
 {
    path: 'my-profile',
    component: ProfileComponent
  },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CarpoolMainRoutingModule { }
