import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Ride } from 'src/app/carpool-main/model/ride.model';
import { BookRideReq } from 'src/app/carpool-main/model/book-ride-req.model';
import { RidesService } from 'src/app/carpool-main/services/rides.service';
import { isEmpty, lastValueFrom, Observable, of } from 'rxjs';
import {timeLabel}  from 'src/assets/static-data/static-data';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-takeride',
  templateUrl: './takeride.component.html',
  styleUrls: ['./takeride.component.css']
})
export class TakeRideComponent implements OnInit {
  
  inputData!:BookRideReq; 
  rides!: Ride[];
  timeSelectedIdx?:number;
  isDropdown=false;
  labels = timeLabel;
  isChecked = true;
  isShowAlert!:boolean;
  message!:string;
  crntDate = new Date();
  constructor(private rideService : RidesService, private router:Router) { }

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
        this.inputData={
          from : this.takeRide.from.value.toLowerCase( ),
          to : this.takeRide.to.value.toLowerCase( ),
          date : this.takeRide.date.value,
          time : this.labels.findIndex(time=> time==this.takeRide.time.value)
        }
        this.rides= await lastValueFrom(this.rideService.getRides(this.inputData));
        if(this.rides.length==0){
          this.isShowAlert=true;
          this.message="No matches Found!!!";
          // this.takeRideForm.reset();
          this.timeSelectedIdx=undefined;
          setTimeout(()=>{
            this.isShowAlert=false;
          },5000); 
        }
    }
    else{
      this.isShowAlert=true;
      this.message="Invalid Inputs!!!"
      setTimeout(()=>{
        this.isShowAlert=false;
      },5000);
    }
  }

  async booking(ride:Ride){
    var res =await lastValueFrom(this.rideService.booking(1,ride.rideId));
    this.takeRideForm.reset();
    this.timeSelectedIdx=undefined;
    this.rides=[];
    this.isShowAlert=true;
    this.message="Ride Booked Successfully!!!"
    setTimeout(()=>{
      this.isShowAlert=false;
    },5000); 
  }

}
