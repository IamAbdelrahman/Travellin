import { Component } from '@angular/core';
import {
  LucideAngularModule,
  Copyright,
  Facebook,
  Twitter,
  Instagram,
} from 'lucide-angular';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [LucideAngularModule],
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss'],
})
export class FooterComponent {
  readonly icons = {
    copyright: Copyright,
    facebook: Facebook,
    twitter: Twitter,
    instagram: Instagram,
  };

  supportLinks = [
    { text: 'Help Center', url: '#' },
    { text: 'Cancellation Options', url: '#' },
    { text: 'Safety Information', url: '#' },
  ];

  nestinLinks = [
    { text: 'About Us', url: '#' },
    { text: 'Careers', url: '#' },
    { text: 'Newsroom', url: '#' },
  ];

  legalLinks = [
    { text: 'Terms', url: '#' },
    { text: 'Privacy', url: '#' },
    { text: 'Sitemap', url: '#' },
  ];
}
