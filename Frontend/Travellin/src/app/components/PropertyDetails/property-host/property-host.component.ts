import { Component, Input, OnInit } from '@angular/core';
import { IPropertyInfo } from '../../../models/domain/iproperty-info';
import { CommonModule } from '@angular/common';
import { 
  BadgeCheck, 
  CalendarDays, 
  CheckCircle2, 
  LucideAArrowDown, 
  LucideAngularModule, 
  Mail, 
  Star, 
} from 'lucide-angular';
import { ActivatedRoute, Router } from '@angular/router';
import { PropertyService } from '../../../services/property.service';
import { ChatService } from '../../../services/chat.service';
import { TokenStorageService } from '../../../services/token-storage.service';
import { ToastService } from '../../../services/toast.service';

@Component({
  selector: 'app-property-host',
  imports: [CommonModule, LucideAngularModule],
  templateUrl: './property-host.component.html',
  styleUrl: './property-host.component.css',
})
export class PropertyHostComponent implements OnInit {
  @Input() property!: IPropertyInfo;
  
  constructor(
    private propertyService: PropertyService,
    private chatService: ChatService,
    private tokenStorage: TokenStorageService,
    private router: Router,
    private toastService: ToastService
  ) {}

  icons = {
    calendar: CalendarDays,
    star: Star,
    check: CheckCircle2,
    verified: BadgeCheck,
    mail: Mail,
  };

  ngOnInit() {
    this.propertyService
      .getPropertyById(this.property?.id)
      .subscribe(property => {
        property.body.owner = {
          ...property.body.owner,
          firstName: property.body.owner.firstName || 'Host',
          photo: {
            id: 'default',
            photoUrl: property.body.owner.photo?.photoUrl || 'logo.png',
          },
          email: '',
          lastName: property.body.owner.lastName,
          phoneNumber: '',
          bio: '',
          birthDate: '',
          userId: property.body.owner.userId,
        };
        this.property = property.body;
      });
  }

  get hostName(): string {
    return `${this.property.owner?.firstName || ''} ${this.property.owner?.lastName || ''}`.trim();
  }

  get isSuperhost(): boolean {
    return (
      this.property.reviewCount >= 10 && this.property.averageRating >= 4.3
    );
  }

  get responseRate(): number {
    return 75;
  }

  contactHost(): void {
    const currentUserId = this.tokenStorage.getUserId();
    const hostId = this.property.owner?.userId;
    
    if (!currentUserId) {
      this.toastService.showError('Please log in to contact the host');
      return;
    }
    
    if (!hostId) {
      this.toastService.showError('Host information not available');
      return;
    }

    if (currentUserId === hostId) {
      this.toastService.showError('You cannot contact yourself');
      return;
    }

    // Start conversation with property context
    this.chatService.startConversation({
      user1Id: currentUserId,
      user2Id: hostId,
      propertyId: this.property.id
    }).subscribe({
      next: (conversation) => {
        // Send initial message about the property
        const initialMessage = `Hi! I'm interested in your property "${this.property.title}". Could you tell me more about it?`;
        
        this.chatService.sendMessage({
          senderId: currentUserId,
          receiverId: hostId,
          content: initialMessage,
          conversationId: conversation.id
        }).subscribe({
          next: () => {
            this.toastService.showSuccess('Message sent to host!');
            this.router.navigate(['/chat'], { 
              queryParams: { conversationId: conversation.id } 
            });
          },
          error: (error) => {
            console.error('Error sending initial message:', error);
            this.toastService.showError('Failed to send initial message');
            // Still navigate to chat even if initial message fails
            this.router.navigate(['/chat'], { 
              queryParams: { conversationId: conversation.id } 
            });
          }
        });
      },
      error: (error) => {
        console.error('Error starting conversation:', error);
        this.toastService.showError('Failed to start conversation with host');
      }
    });
  }
}