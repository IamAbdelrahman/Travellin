import { Component, inject, OnInit, OnDestroy } from '@angular/core';
import { ToastService } from '../../services/toast.service';
import { CommonModule } from '@angular/common';
import { LucideAngularModule, X } from 'lucide-angular';
import { Subscription } from 'rxjs';
import { ToastMessage } from '../../services/toast.service';

@Component({
  selector: 'app-toast-container',
  standalone: true,
  imports: [CommonModule, LucideAngularModule],
  templateUrl: './toast-container.component.html',
  styleUrl: './toast-container.component.scss',
})
export class ToastContainerComponent implements OnInit, OnDestroy {
  toastService = inject(ToastService);
  icons = { x: X };
  
  toasts: ToastMessage[] = [];
  private subscription: Subscription = new Subscription();

  ngOnInit() {
    this.subscription = this.toastService.toast$.subscribe(toast => {
      if (toast) {
        this.toasts.push(toast);
        // Auto-remove toast after 5 seconds
        setTimeout(() => {
          this.removeToast(toast);
        }, 5000);
      }
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  removeToast(toast: ToastMessage) {
    const index = this.toasts.indexOf(toast);
    if (index > -1) {
      this.toasts.splice(index, 1);
    }
  }

  getToastClass(type: string): string {
    switch (type) {
      case 'success':
        return 'toast-success text-white';
      case 'error':
        return 'toast-danger text-white';
      case 'warning':
        return 'toast-warning text-white';
      case 'info':
        return 'toast-info text-white';
      default:
        return 'toast-info text-white';
    }
  }
}
