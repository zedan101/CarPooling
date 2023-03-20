import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlertMsgComponent } from './components/alert-msg/alert-msg.component';



@NgModule({
  declarations: [
    AlertMsgComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    AlertMsgComponent
  ],
})
export class CommonFeaturesModule { }
