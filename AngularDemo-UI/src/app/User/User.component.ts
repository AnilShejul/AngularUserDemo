import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-User',
  templateUrl: './User.component.html',
  styleUrls: ['./User.component.css']
})
export class UserComponent implements OnInit {

  users:any;
  constructor(private http:HttpClient) { }

  ngOnInit() {
    this.getUsers();
  }

  getUsers()
  {
    this.http.get('http://localhost:5000/api/values').subscribe(response =>
    {
        this.users = response;
    },
    error =>
    {
      console.log(error);
    }
  );
  }
}
