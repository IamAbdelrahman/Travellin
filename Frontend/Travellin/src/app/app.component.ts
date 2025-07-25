// src/app/app.component.ts
import { Component, OnInit, OnDestroy } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UpperCasePipe } from '@angular/common';
import { ChatService } from './services/chat.service';
import { TokenStorageService } from './services/token-storage.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, UpperCasePipe],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class App implements OnInit, OnDestroy {
  protected title = 'Travellin';

  constructor(
    private chatService: ChatService,
    private tokenStorage: TokenStorageService
  ) {}

  async ngOnInit() {
    const token = this.tokenStorage.getAccessToken();
    if (token && this.tokenStorage.isTokenValid()) {
      try {
        await this.chatService.startConnection();
        console.log('Chat service initialized successfully');
      } catch (error) {
        console.error('Failed to start chat connection:', error);
      }
    }
  }

  ngOnDestroy() {
    this.chatService.stopConnection();
  }
}