import { Component, OnInit } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Ride } from 'src/app/carpool-main/model/ride.model';
import { RidesService } from 'src/app/carpool-main/services/rides.service';

@Component({
  selector: 'app-my-rides',
  templateUrl: './my-rides.component.html',
  styleUrls: ['./my-rides.component.css']
})
export class MyRidesComponent implements OnInit {

  bookedRides:Array<Ride>=[];
  offeredRides:Array<Ride>=[];
  tempRide:Ride=new Ride();

  constructor(private rideService:RidesService) { }

  async ngOnInit(){
    var bookRides =  await lastValueFrom(this.rideService.getBookedHistory());
    this.offeredRides = await lastValueFrom(this.rideService.getOOfferedHistory());
    bookRides.forEach(rides=>
      rides.rideTakenBy.forEach(element => {
        this.tempRide = rides;
        this.tempRide.rideTakenBy.forEach(e => e=element);
        this.bookedRides.push(this.tempRide);
      }))
  }

}
