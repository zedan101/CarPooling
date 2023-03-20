import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EntryFormsComponent } from './components/entryforms/entryforms.component';
import { AuthRoutingModule } from './auth-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthComponent } from './auth.component';


@NgModule({
  declarations: [EntryFormsComponent, AuthComponent],
  imports: [NgbModule,
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    AuthRoutingModule
  ]
})
export class AuthModule { }
