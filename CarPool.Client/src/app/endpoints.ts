import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class Endpoints{
    constructor() { }

    static AuthLogin =class{
      static AuthenticateLogin(userEmail:string,password:string){
          return `${environment.localhostLogin}/Login?userEmail=${userEmail}&password=${encodeURIComponent(password)}`;
      }
    }

    static Users =class{

        static getUserDetails(){
            return `${environment.localhostUsers}/GetUserDetails`;
          }
        
        static getUser(userId:string)
          {
            return `${environment.localhostUsers}/GetUsers?userId=${userId}`;
          }
        
        static postUsers()
          {
            return `${environment.localhostUsers}/PostUser`;
          }
        
        // static getValidation(userEmail:string,password:string){
        //     return `${environment.localhostUsers}/ValidateUser?userEmail=${userEmail}&password=${password}`;
        //   }
        
        static getEmailValidation(userEmail:string){
            return `${environment.localhostUsers}/ValidateEmail?userEmail=${userEmail}`;
          }
    }

    static Rides = class{
        static getRides(date : Date , time:number, startLocation:string , destination:string){
            return `${environment.localhostRides}/GetRideMatches?date=${date}&time=${time}&startLocation=${startLocation}&destination=${destination}`;
        }

        static pushRide(){
            return `${environment.localhostRides}/PushRide`;
        }

        static getBookedHistory(){
            return `${environment.localhostRides}/GetBookedHistory`;
        }

        static getOfferedHistory(){
          return `${environment.localhostRides}/GetOfferedHistory`;
        }
        
        static booking(seat:number,rideId:string){
          return `${environment.localhostRides}/Booking?seats=${seat}&rideId=${rideId}`;
        }
    }
}