import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app.routing.module';
import { DatePipe } from '@angular/common';
import { TokenInterceptorService } from './common/services/token-interceptor.service';
import { CommonFeaturesModule } from "./common/common-features.module";

@NgModule({
    declarations: [
        AppComponent,
    ],
    providers: [DatePipe, {
            provide: HTTP_INTERCEPTORS,
            useClass: TokenInterceptorService,
            multi: true,
        },],
    bootstrap: [AppComponent],
    imports: [
        BrowserModule, HttpClientModule, NgbModule,
        ReactiveFormsModule,
        FormsModule,
        AppRoutingModule,
        CommonFeaturesModule
    ]
})
export class AppModule { }
