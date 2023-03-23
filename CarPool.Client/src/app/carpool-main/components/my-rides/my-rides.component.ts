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
  bookRides:Array<Ride>=[];
  constructor(private rideService:RidesService) { }

  ngOnInit(){
    
    this.rideService.getBookedHistory().subscribe(data =>{
      data.forEach(obj=> obj.rideTakenBy.forEach(element => {
        this.tempRide = obj;
        this.tempRide.rideTakenBy.forEach(e => e=element);
        this.bookedRides.push(this.tempRide);
      }));
    }
    );
    this.rideService.getOOfferedHistory().subscribe(data=>{
      data.forEach(obj=> this.offeredRides.push(obj));
    });
  }

}
