import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Endpoints } from '../../endpoints';
import { Ride } from '../model/ride.model';
import { BookRideReq } from '../model/book-ride-req.model';


@Injectable({
  providedIn: 'root'
})
export class RidesService {
    constructor(private httpService: HttpClient) { }

    headers = new HttpHeaders({
        'Content-Type': 'application/json'
      });

    getRides(ride:BookRideReq){
        return this.httpService.get<Array<Ride>>(Endpoints.Rides.getRides(ride.date,ride.time,ride.from,ride.to));
    }

    offerRide(ride : Ride){
        return this.httpService.post<boolean>(Endpoints.Rides.pushRide(),ride,{headers: this.headers});
    }

    getBookedHistory(){
        return this.httpService.get<Array<Ride>>(Endpoints.Rides.getBookedHistory());
    }
    
    getOOfferedHistory(){
        return this.httpService.get<Array<Ride>>(Endpoints.Rides.getOfferedHistory());
    }

    booking(seats:number,rideId:string){
        return this.httpService.post<boolean>(Endpoints.Rides.booking(seats,rideId),"");
    }
}
