import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { OfferRideComponent } from './components/offerride/offerride.component';
import { TakeRideComponent } from './components/takeride/takeride.component';
import { MyRidesComponent } from './components/my-rides/my-rides.component';

const routes: Routes = [
  {
    path: '',
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
    path: 'take-ride/my-rides',
    component: MyRidesComponent
  },
  {
    path: 'offer-ride/my-rides',
    component: MyRidesComponent
  },
  {
    path: 'my-rides',
    component: MyRidesComponent
  },
  {
    path: 'take-ride/my-profile',
    component: HomeComponent
  }, {
    path: 'offer-ride/my-profile',
    component: HomeComponent
  }, {
    path: 'my-profile',
    component: HomeComponent
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CarpoolMainRoutingModule { }
