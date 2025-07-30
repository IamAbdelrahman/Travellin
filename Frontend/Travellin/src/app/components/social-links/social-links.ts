import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountService } from '../../services/account.service';
@Component({
  standalone: true,
  selector: 'app-social-links',
  imports: [CommonModule],
  templateUrl: './social-links.html',
  styleUrls: ['./social-links.css']  
})
export class SocialLinksComponent {
  @Input() emailOption = false;
  @Input() phoneOption = true;

  @Output() emailOptionChange = new EventEmitter<boolean>();
  @Output() phoneOptionChange = new EventEmitter<boolean>();
  constructor(private auth: AccountService) {}
  signInGoogle() {
    this.auth.continueWithGoogle().then(() => {
      console.log('Google sign-in successful');
    }).catch((error) => {
      console.error('Google sign-in failed', error);
    });
}

  continueWithFacebook() {
    console.log('Continue with Facebook clicked');
  }

  continueWithEmail() {
    console.log('Continue with Email');
    this.emailOption = true;
    this.phoneOption = false;
    this.emailOptionChange.emit(this.emailOption);
    this.phoneOptionChange.emit(this.phoneOption);
  }

  continueWithPhoneNumber() {
    console.log('Continue with Phone Number');
    this.phoneOption = true;
    this.emailOption = false;
    this.phoneOptionChange.emit(this.phoneOption);
    this.emailOptionChange.emit(this.emailOption);
  }
}
