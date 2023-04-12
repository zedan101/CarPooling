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

        static changePassword(newPassword:string)
        {
            return `${environment.localhostUsers}/ChangePassword?newPass=${encodeURIComponent(newPassword)}`;
        }
        
        static updateProfile()
        {
            return `${environment.localhostUsers}/UpdateProfile`;
        }

        static deleteProfile(){
          return `${environment.localhostUsers}/DeleteProfile`;
        }

        static getEmailValidation(userEmail:string){
            return `${environment.localhostUsers}/ValidateEmail?userEmail=${userEmail}`;
          }
    }

    static Rides = class{
        static getRides(date : Date , time:number, startLocation:string , destination:string){
            return `${environment.localhostRides}/RideMatches?date=${date}&time=${time}&startLocation=${startLocation}&destination=${destination}`;
        }

        static pushRide(){
            return `${environment.localhostRides}/OfferARide`;
        }

        static getBookedHistory(){
            return `${environment.localhostRides}/BookedHistory`;
        }

        static getOfferedHistory(){
          return `${environment.localhostRides}/OfferedHistory`;
        }
        
        static booking(seat:number,rideId:string,startLocation:string,endLocation:string){
          return `${environment.localhostRides}/Booking?seats=${seat}&rideId=${rideId}&startLocation=${startLocation}&endLocation=${endLocation}`;
        }
    }
}