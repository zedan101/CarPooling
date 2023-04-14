import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastComponent } from './components/toast/toast.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoaderComponent } from './components/loader/loader.component';


@NgModule({
  declarations: [
    ToastComponent,
    LoaderComponent
  ],
  imports: [
    CommonModule,
    NgbModule
  ],
  exports: [
    ToastComponent,
    LoaderComponent
  ],
})
export class CommonFeaturesModule { }
