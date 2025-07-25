// src/app/services/toast.service.ts
import { Injectable, inject } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

export type ToastType = 'success' | 'danger' | 'warning' | 'info';

export interface Toast {
  message: string;
  type: ToastType;
  delay?: number;
}

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  private sanitizer = inject(DomSanitizer);
  private toasts: Toast[] = [];

  getToasts() {
    return this.toasts;
  }

  show(message: string, type: ToastType = 'info', delay: number = 5000): void {
    const toast: Toast = {
      message: this.sanitizer.sanitize(1, message) || '',
      type,
      delay,
    };
    this.toasts.push(toast);

    if (delay > 0) {
      setTimeout(() => this.remove(toast), delay);
    }
  }

  remove(toast: Toast): void {
    this.toasts = this.toasts.filter(t => t !== toast);
  }

  clear(): void {
    this.toasts = [];
  }

  // Convenience methods
  showSuccess(message: string, delay: number = 5000): void {
    this.show(message, 'success', delay);
  }

  showError(message: string, delay: number = 5000): void {
    this.show(message, 'danger', delay);
  }

  showWarning(message: string, delay: number = 5000): void {
    this.show(message, 'warning', delay);
  }

  showInfo(message: string, delay: number = 5000): void {
    this.show(message, 'info', delay);
  }
}
