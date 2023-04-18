import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { lastValueFrom, take } from 'rxjs';
import { AuthService } from 'src/app/auth/services/auth.service';
import { ImageCompressorService } from 'src/app/common/services/image-compressor.service';
import { User } from '../../model/user.model';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  imageUrl!:string;
  user!:User;
  isShowThumbnail=false;
  constructor(private userService:UsersService,private modalService: NgbModal,private authService:AuthService,private router:Router,private compressImage : ImageCompressorService) { }

  async ngOnInit(){
    this.user=await lastValueFrom(this.userService.getUserDetails());
    this.imageUrl=this.user.profileImage;
    this.updtProf.profileImage.setValue(this.user.profileImage);
    this.updtProf.newName.setValue(this.user.userName);
  }

  updateProfile : any = new FormGroup ({
    profileImage:new FormControl(''),
    newName: new FormControl('')
  })

  updatePassword:any = new FormGroup({
    newPassword : new FormControl('',[Validators.required]),
    confirmPass: new FormControl('',[Validators.required])
  })
  openVerticallyCentered(formModal: any) {
    this.modalService.open(formModal, { centered: true });
  }

  get updtPass(){return this.updatePassword.controls;}
  get updtProf(){return this.updateProfile.controls;}

  async saveNewPass(){
    if(this.updtPass.newPassword.value==this.updtPass.confirmPass.value){
      var response=await lastValueFrom(this.userService.updatePassword(this.updtPass.newPassword.value));
      if(response){
        this.closeModal();
      }
    }
  }

  async saveProfileData(){
    if(this.updateProfile.valid){
      this.user.userName=this.updtProf.newName.value;
      this.user.profileImage=this.imageUrl;
      var response=await lastValueFrom(this.userService.updateProfile(this.user))
      if(response){
        this.closeModal();
      }
    }
  }

  async deleteAccount(res:boolean){
    if(res){
      var response = await lastValueFrom(this.userService.deleteProfile());
      if(response){
        this.closeModal()
        this.authService.logout();
      }
    }
    else{
      this.closeModal();
    }
  }

  closeModal(): void {
    this.modalService.dismissAll();
  }
  
  selectImage(e: any) {
    if (e.target.files) {
      var reader = new FileReader();
      this.compressImage.compress(e.target.files[0],200,200)
      .pipe(take(1))
      .subscribe(compressedImage => {
        console.log(`Image size after compressed: ${compressedImage.size} bytes.`)
        reader.readAsDataURL(compressedImage);
        reader.onload = (event: any) => {
          console.log(event.target.result);
          this.imageUrl = event.target.result;
        };
      })
     
    }
  }
}
