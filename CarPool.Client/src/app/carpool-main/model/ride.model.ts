export class Ride
{
    location:Array<string>=[];
    date!:Date; 
    time!:number;
    numberOfSeatsAvailable!:number;
    price!:number;
    rideId!:string;
    rideOfferedBy!:string;
    rideTakenBy!:Array<string>;
}