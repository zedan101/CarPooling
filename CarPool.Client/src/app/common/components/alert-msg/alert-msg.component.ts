import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-alert-msg',
  templateUrl: './alert-msg.component.html'
})
export class AlertMsgComponent implements OnInit {
  @Input() isShowAlert!:boolean;
  @Input() message!:string;
  constructor() { }

  ngOnInit(): void {
  }

}
