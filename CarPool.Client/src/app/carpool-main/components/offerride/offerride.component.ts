import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { Ride } from 'src/app/carpool-main/model/ride.model';
import { RidesService } from 'src/app/carpool-main/services/rides.service';
import { timeLabel,seatLabel } from 'src/assets/static-data/static-data';

@Component({
  selector: 'app-offerride',
  templateUrl: './offerride.component.html',
  styleUrls: ['./offerride.component.css']
})

export class OfferRideComponent implements OnInit {
  isNextPressed:boolean=false;
  userName!:string;
  imgLink!:string;
  seatSelectedIdx?:number;
  timeSelectedIdx?:number;
  generatedPrice = 180;
  isDropdown = false;
  labels = timeLabel;
  seatsLabel = seatLabel;
  isChecked=false;
  isShowAlert!:boolean;
  message!:string;
  constructor(private fb: FormBuilder,private rideService: RidesService,private router:Router) { }

  ngOnInit(): void {
  }
  offerRideForm: any = new FormGroup({
    from: new FormControl('',[Validators.required]),
    to: new FormControl('', [Validators.required]),
    date: new FormControl('', [
      Validators.required,
    ]),
    time: new FormControl('',[Validators.required]),
    seats: new FormControl('',[Validators.required]),
    //price: new FormControl('',[Validators.required]),
    stops: this.fb.array([this.fb.control('',[Validators.required])])
  });

  toogleRide(){
     this.router.navigate(['carpool/take-ride']);

  }
  
  
  get rideOffer() { return this.offerRideForm.controls; };


  setSeat(seats:string,index:number){
    this.seatSelectedIdx=index;
    this.rideOffer.seats.setValue(seats);
  }

  addStops(){

    this.rideOffer.stops.push(this.fb.control('',[Validators.required]));
    
  }

  minusStops(){
      this.rideOffer.stops.removeAt(this.offerRideForm.controls['stops'].length-1);
    }
  
  async offerRide() {
    if (this.offerRideForm.valid) {
      let ride = new Ride();
      let formArrVal = this.rideOffer.stops.value;
      ride.location.push(this.rideOffer.from.value.toLowerCase( ));
      if (formArrVal[0] != "") {
        formArrVal.forEach((val: string) => {
          ride.location.push(val.toLowerCase( ));
        });
      }
      ride.location.push(this.rideOffer.to.value.toLowerCase( ));
      ride.date = new Date(this.rideOffer.date.value);
      ride.time = this.labels.indexOf(this.rideOffer.time.value);
      ride.numberOfSeatsAvailable = this.seatsLabel.indexOf(this.rideOffer.seats.value) + 1;
      ride.price = 180;
      ride.rideId = "";
      await lastValueFrom(this.rideService.offerRide(ride));
      this.offerRideForm.reset();
      this.isShowAlert = true;
      this.message="Ride Created Successfully!!!"
      setTimeout(() => {
        this.isShowAlert = false;
      }, 5000);
      this.isNextPressed=false;
    }
    else{
      
      this.isShowAlert = true;
      this.message="Invalid Inputs"
      setTimeout(() => {
        this.isShowAlert = false;
      }, 5000);
    }

     
  }

}
