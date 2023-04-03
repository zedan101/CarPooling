import { Component, OnInit } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Ride } from 'src/app/carpool-main/model/ride.model';
import { RidesService } from 'src/app/carpool-main/services/rides.service';
import { RideResponse } from '../../model/ride-response.model';

@Component({
  selector: 'app-my-rides',
  templateUrl: './my-rides.component.html',
  styleUrls: ['./my-rides.component.css']
})
export class MyRidesComponent implements OnInit {

  bookedRides:Array<RideResponse>=[];
  offeredRides:Array<RideResponse>=[];
  tempRide:RideResponse=new RideResponse();
  bookRides:Array<RideResponse>=[];
  constructor(private rideService:RidesService) { }

  async ngOnInit(){
    this.bookedRides = await lastValueFrom(this.rideService.getBookedHistory());
    this.offeredRides =await lastValueFrom(this.rideService.getOOfferedHistory());
  }

}
