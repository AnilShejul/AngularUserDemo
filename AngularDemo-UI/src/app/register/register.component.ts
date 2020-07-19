import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../Service/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};
  @Output() cancelRegister = new EventEmitter();
  constructor(private authService:AuthService) { }

  ngOnInit() {
  }

  register()
  {  
    this.authService.register(this.model).subscribe(()=>
    {
console.log('Register succussful.');
this.cancelRegister.emit(false);
    },
    error=>
    {
      console.log('Register failed.');
    }
    );

  }

  cancel()
    {
      this.cancelRegister.emit(false);
    }

}
