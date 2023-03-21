import { Component, OnInit } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { User } from 'src/app/carpool-main/model/user.model';
import { AuthService } from 'src/app/auth/services/auth.service';
import { UsersService } from 'src/app/carpool-main/services/users.service';

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
  constructor(private authService:AuthService,private userService:UsersService) { }

async  ngOnInit(){
  this.loggedUser = await lastValueFrom(this.userService.getUserDetails())
  }

  logOut(){
    this.authService.logout();
  }
}
