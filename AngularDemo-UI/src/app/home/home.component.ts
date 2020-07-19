import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  IsRegisterMode = false;
  constructor() { }

  ngOnInit() {
  }

  registerToggle()
  {
this.IsRegisterMode = true;
  }

  CancelRegisterMode(IsRegisterMode: boolean)
  {
    this.IsRegisterMode = IsRegisterMode;
  }

}
