import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { UsersService } from '../../services/users.service';
import { UserProfiles, User } from '../../models/api/response/iget-users';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-users-admin',
  imports: [CommonModule, RouterModule],
  templateUrl: './users-admin.component.html',
  styleUrl: './users-admin.component.scss',
})
export class UsersAdminComponent implements OnInit {
  users: User[] = [];
  constructor(private userService: UsersService) {}
  ngOnInit(): void {
    this.loadUsers();
  }
  loadUsers(): void {
    this.userService.getUsers().subscribe({
      next: (response: HttpResponse<UserProfiles>) => {
        this.users = response?.body?.items || [];
        console.log(response, 'no response');
      },
      error: err => {
        console.error('Error loading data', err);
      },
    });
  }
}
