import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Ride } from 'src/app/carpool-main/model/ride.model';
import { BookRideReq } from 'src/app/carpool-main/model/book-ride-req.model';
import { RidesService } from 'src/app/carpool-main/services/rides.service';
import { isEmpty, lastValueFrom, Observable, of } from 'rxjs';
import {timeLabel}  from 'src/assets/static-data/static-data';
import { Route, Router } from '@angular/router';
import { ToastService } from 'src/app/common/services/toast.service';
import { RideResponse } from '../../model/ride-response.model';

@Component({
  selector: 'app-takeride',
  templateUrl: './takeride.component.html',
  styleUrls: ['./takeride.component.css']
})
export class TakeRideComponent implements OnInit {
  
  inputData!:BookRideReq; 
  rides!: RideResponse[];
  timeSelectedIdx?:number;
  isDropdown=false;
  labels = timeLabel;
  isChecked = true;
  crntDate = new Date();
  constructor(private rideService : RidesService, private router:Router,private toastService:ToastService) { }

  ngOnInit(): void {
  }

  takeRideForm: any = new FormGroup({
    from: new FormControl('',[Validators.required]),
    to: new FormControl('',[Validators.required]),
    date: new FormControl('', [
      Validators.required,
    ]),
    time: new FormControl('',[Validators.required])
  });


  get takeRide() { return this.takeRideForm.controls; };
  toogleRide(){
 
      this.router.navigate(['carpool/offer-ride']);
  }

  async getMatches(){
    if(this.takeRideForm.valid){
      if(this.takeRide.from.value.toLowerCase()==this.takeRide.to.value.toLowerCase()){
        this.toastService.show("Source And Destination Can't be same!!!", { classname: 'background-yellow text-light'});
      }
      else{
        this.inputData={
          from : this.takeRide.from.value.toLowerCase( ),
          to : this.takeRide.to.value.toLowerCase( ),
          date : this.takeRide.date.value,
          time : this.labels.findIndex(time=> time==this.takeRide.time.value)
        }
        this.rides= await lastValueFrom(this.rideService.getRides(this.inputData));
        if(this.rides.length==0){
          this.toastService.show("No matches Found!!!", { classname: 'background-yellow text-light'});
        }
      }
    }
    else{
    this.toastService.show("Invalid Input", { classname: 'bg-danger text-light'});
    }
  }

  async booking(ride:RideResponse){
    var res =await lastValueFrom(this.rideService.booking(1,ride.rideId,this.inputData.from,this.inputData.to));
    this.takeRideForm.reset();
    this.timeSelectedIdx=undefined;
    this.rides=[];
    this.toastService.show("Ride Booked Successfully!!!", { classname: 'bg-success text-light'});
  }

}
