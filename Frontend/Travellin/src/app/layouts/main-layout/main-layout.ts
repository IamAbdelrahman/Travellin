import { Component } from '@angular/core';
import {HeaderComponent} from '../../features/shared/components/header/header';
import {FooterComponent} from '../../features/shared/components/footer/footer';
import { Router } from 'lucide-angular';
import { RouterOutlet } from '@angular/router';
import { ToastContainerComponent } from '../../features/shared/toast-container/toast-container';
@Component({
  selector: 'app-main-layout',
  imports: [HeaderComponent, FooterComponent, RouterOutlet, ToastContainerComponent],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.css'
})
export class MainLayoutComponent {

}
