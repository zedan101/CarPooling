import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Endpoints } from '../../endpoints';
import { User } from '../model/user.model';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private httpService: HttpClient) { }

  getUserDetails(){
    return this.httpService.get<User>(Endpoints.Users.getUserDetails());
  }

  getUsers(userId:string)
  {
    return this.httpService.get<User>(Endpoints.Users.getUser(userId));
  }

  updatePassword(newPassword : string)
  {
    return this.httpService.patch<boolean>(Endpoints.Users.changePassword(newPassword),"");
  }

  updateProfile(user:User){
    return this.httpService.patch<boolean>(Endpoints.Users.updateProfile(),user);
  }

  deleteProfile(){
    return this.httpService.delete<boolean>(Endpoints.Users.deleteProfile());
  }

  postUsers(user : User)
  {
    return this.httpService.post<Boolean>(Endpoints.Users.postUsers(), user);
  }

  // getValidation(userEmail:string,password:string){
  //   return this.httpService.get<Boolean>(Endpoints.Users.getValidation(userEmail,password));
  // }

  getEmailValidation(userEmail:string){
      return this.httpService.get<Boolean>(Endpoints.Users.getEmailValidation(userEmail));
  }
}