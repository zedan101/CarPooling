import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { OfferRideComponent } from './components/offerride/offerride.component';
import { TakeRideComponent } from './components/takeride/takeride.component';
import { MyRidesComponent } from './components/my-rides/my-rides.component';
import { ProfileComponent } from './components/profile/profile.component';
import { AuthGuardGuard } from '../common/components/auth-guard.guard';

const routes: Routes = [
  
  { path: '', redirectTo: 'home',pathMatch:'full'},
  {
    path: 'home',
    component: HomeComponent,
    canActivate:[AuthGuardGuard]
  },
  {
    path: 'offer-ride',
    component: OfferRideComponent,
    canActivate:[AuthGuardGuard]
  },
  {
    path: 'take-ride',
    component: TakeRideComponent,
    canActivate:[AuthGuardGuard]
  },
  {
    path: 'my-rides',
    component: MyRidesComponent,
    canActivate:[AuthGuardGuard]
  },
 {
    path: 'my-profile',
    component: ProfileComponent,
    canActivate:[AuthGuardGuard]
  },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CarpoolMainRoutingModule { }
