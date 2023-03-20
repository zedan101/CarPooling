import { Component, OnInit } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { User } from 'src/app/carpool-main/model/user.model';
import { UsersService } from 'src/app/carpool-main/services/users.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  userName!:string;
  imgLink!:string;
  userList!:Array<User>;
  isDropdown=false;

  constructor(private userService : UsersService) { }

  async ngOnInit(){
    this.userName="Nitish";
    this.imgLink="../../../assets/images/logo.png";
  }

}
