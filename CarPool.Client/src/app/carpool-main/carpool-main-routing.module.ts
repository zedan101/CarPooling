import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { OfferRideComponent } from './components/offerride/offerride.component';
import { TakeRideComponent } from './components/takeride/takeride.component';
import { MyRidesComponent } from './components/my-rides/my-rides.component';
import { ProfileComponent } from './components/profile/profile.component';

const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path:'home/offer-ride',
    redirectTo:'offer-ride'
  },
  {
    path:'home/take-ride',
    redirectTo:'take-ride'
  },
  {
    path:'home/my-rides',
    redirectTo:'my-rides'
  },
  {
    path:'home/my-profile',
    redirectTo:'my-profile'
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
    path:'my-profile/my-rides',
    redirectTo:'my-rides'
  },
  {
    path: 'take-ride/my-rides',
    redirectTo:'my-rides'
  },
  {
    path: 'offer-ride/my-rides',
    redirectTo:'my-rides'
  },
  {
    path: 'my-rides',
    component: MyRidesComponent,
  },
  {
    path: 'my-rides/my-profile',
    redirectTo:'my-profile'
  },
  {
    path: 'take-ride/my-profile',
    redirectTo:'my-profile'
  }, {
    path: 'offer-ride/my-profile',
    redirectTo:'my-profile'
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
