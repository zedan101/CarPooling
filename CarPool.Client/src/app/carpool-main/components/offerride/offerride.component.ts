import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbToast } from '@ng-bootstrap/ng-bootstrap';
import { lastValueFrom } from 'rxjs';
import { Ride } from 'src/app/carpool-main/model/ride.model';
import { RidesService } from 'src/app/carpool-main/services/rides.service';
import { ToastService } from 'src/app/common/services/toast.service';
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
  crntDate = new Date();
  @ViewChild('toast') toaster!: ElementRef;
  constructor(private fb: FormBuilder,private rideService: RidesService,private router:Router,public toastService: ToastService) { }

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
      const distinctionTestSet =new Set(ride.location)
      if(ride.location.length==distinctionTestSet.size){
        ride.date = new Date(this.rideOffer.date.value);
        ride.time = this.labels.indexOf(this.rideOffer.time.value);
        ride.numberOfSeatsAvailable = this.seatsLabel.indexOf(this.rideOffer.seats.value) + 1;
        ride.price = 180;
        ride.rideId = "";
        var res=await lastValueFrom(this.rideService.offerRide(ride));
        if(res){
          this.toastService.show("Ride Created Successfully!!!", { classname: 'bg-success text-light'});
          this.offerRideForm.reset();
          this.isNextPressed=false;
        }else{
          this.toastService.show("Something Went Wrong...", { classname: 'bg-danger text-light'});
        }
      }
      else{
        this.toastService.show("Any Two Locations Cant be same!!!", { classname: 'bg-danger text-light'});  
      }
    }
    else{
      this.toastService.show("Invalid Inputs", { classname: 'bg-danger text-light'});
    }

     
  }

}
