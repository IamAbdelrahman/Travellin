import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { UsersService } from '../../services/users.service';
import { UserProfiles, User } from '../../models/api/response/iget-users';
import { HttpResponse } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ToastService } from '../../services/toast.service'; // Adjust the path as needed
import {
  faTrash,
} from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
@Component({
  selector: 'app-users-admin',
  imports: [CommonModule, RouterModule, FontAwesomeModule, FormsModule],
  templateUrl: './users-admin.component.html',
  styleUrl: './users-admin.component.scss',
})
export class UsersAdminComponent implements OnInit {
  users: User[] = [];
  filtered: User[] = [];
  @Output() adminPhotoEvent = new EventEmitter<string>();
  adminPhoto:string = '';
  loading: boolean = false;
  selectedFilter = 'all';
  searchTerm = '';
  icons: { [key: string]: any } = {
    trash: faTrash,
  };
  constructor(private userService: UsersService, private toastService: ToastService) {}
  ngOnInit(): void {
    this.loadUsers();
  }
  loadUsers(): void {
    this.userService.getUsers().subscribe({
      next: (response: HttpResponse<UserProfiles>) => {
        this.users = response?.body?.items || [];
        this.adminPhoto = this.getAdminPhoto();
        this.adminPhotoEvent.emit(this.adminPhoto);
        this.getFilteredUsers();
        console.log("Bio", this.users[0].bio)
      },
      error: err => {
        console.error('Error loading data', err);
      },
    });
  }

  toggleUserStatus(users: User): void {
    users.status = users.status === 'Active' ? 'Blocked' : 'Active';
  }
  getFilteredUsers(): User[] {
     this.filtered = this.users;
    
    if (this.selectedFilter !== 'all') {
      this.filtered = this.filtered.filter(user => user.roles[0].toLowerCase() === this.selectedFilter);
    }
    
    if (this.searchTerm) {
      this.filtered = this.filtered.filter(users => 
        users.userName.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        users.email.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    }
    
    return this.filtered;
  }
  getStatusColor(status: string): string {
      switch (status.toLowerCase()) {
        case 'active':
        case 'approved':
        case 'confirmed':
        case 'completed':
          return 'text-green-600 bg-green-100';
        case 'pending':
          return 'text-yellow-600 bg-yellow-100';
        case 'blocked':
        case 'rejected':
        case 'cancelled':
        case 'failed':
          return 'text-red-600 bg-red-100';
        default:
          return 'text-gray-600 bg-gray-100';
      }
  }
  getAdminPhoto():string {
    for (let i = 0; i < this.users.length; i++) {
      if (this.users[i].roles[0] == 'Admin') {
        return this.users[i].photo.photoUrl;
      }
      else continue;
    }
    return '';
  }
  onDelete(id: string): void {
    const userToDelete = this.users.find(u => u.userId === id);
    if (!userToDelete) return;
    if (userToDelete.status === "Blocked") {
      userToDelete.status = "Active";
      return;
    }

    if (confirm('Are you sure you want to block this user?')) {
      this.userService.deleteUser(id).subscribe({
        next: () => {
          console.log('Trying to block user with ID:', id); // to test
          this.users = this.users.filter(u => u.userId !== id);
          this.toastService.showSuccess('User blocked successfully');
          window.location.reload();
        },
        error: err => {
          console.error('Error blocking user', err);
          this.toastService.showError('Failed to block user');
        },
      });
    }
  }
}
