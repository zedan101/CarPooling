import { Component, OnInit } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { User } from 'src/app/carpool-main/model/user.model';
import { AuthService } from 'src/app/auth/services/auth.service';
import { UsersService } from 'src/app/carpool-main/services/users.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.css']
})
export class TopMenuComponent implements OnInit {

  isDropdown = false;
  userName="Nitish";
  imgLink="../../../assets/images/logo.png";
  loggedUser!:User;
  isShowThumbnail=false;
  constructor(private authService:AuthService,private userService:UsersService, private router:Router) { }

ngOnInit(){
  this.userService.getUserDetails().subscribe(data=>{
    this.loggedUser=data;
  })
  }

  openMyProfile(){
    this.router.navigate(['carpool/my-profile'])
  }
  openMyRides(){
    this.router.navigate(['carpool/my-rides'])
  }

  logOut(){
    this.authService.logout();
  }
}
