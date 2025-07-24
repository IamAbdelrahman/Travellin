import { Component, inject } from '@angular/core';
import { ToastService } from '../services/toast-containe.service'
import { CommonModule } from '@angular/common';
import { LucideAngularModule, X } from 'lucide-angular';

@Component({
  selector: 'app-toast-container',
  standalone: true,
  imports: [CommonModule, LucideAngularModule],
  templateUrl: './toast-container.html',
  styleUrl: './toast-container.scss',
})
export class ToastContainerComponent {
  toastService = inject(ToastService);
  icons = { x: X };
}