import { Component } from '@angular/core';
import { LoginComponentForm } from '../../components/login/login';
@Component({
  selector: 'app-login-page',
  imports: [LoginComponentForm],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.css'
})
export class LoginPageComponent {

}
