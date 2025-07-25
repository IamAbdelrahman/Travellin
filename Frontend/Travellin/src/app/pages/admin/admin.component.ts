import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { UsersService } from '../../services/users.service';
import { User } from '../../models/api/response/iget-users';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-admin',
  imports: [RouterModule, CommonModule],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss',
})
export class AdminComponent {
  //collapse sidebar
  //  isCollapsed = false;
  //  toggleSidebar() {
  //    this.isCollapsed = !this.isCollapsed;
  //  }
  // users : User []=[];
  //  constructor(private userService:UsersService){}
  //  ngOnInit(): void {
  //    this.loadUsers();
  //  }
  // loadUsers(){
  //   this.userService.getUsers().subscribe({
  //     next: (res : HttpResponse<User>) => {
  //       this.users= [res.body!] ;
  //     },
  //     error:(err) => {
  //       console.error('Error loading data',err);
  //     }
  //   })
  // }
}
