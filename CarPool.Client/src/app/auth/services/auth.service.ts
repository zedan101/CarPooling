import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Endpoints } from '../../endpoints';
import { AuthResponse } from '../models/auth-response.model';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
    constructor(private httpService:HttpClient,private router : Router){}

    getAuthentication(userEmail:string , password:string){
        return this.httpService.get<AuthResponse>(Endpoints.AuthLogin.AuthenticateLogin(userEmail,password));
    }

    logout(){
      window.localStorage.clear();
      if(window.localStorage.getItem("access_token")==null){
        this.router.navigate(['auth']);
      }
      else{
        this.logout();
      }
    }

  
}