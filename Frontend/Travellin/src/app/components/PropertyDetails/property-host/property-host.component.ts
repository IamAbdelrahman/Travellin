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
    console.log('Property Host Component - Initial property:', this.property);
    console.log('Property Host Component - Owner from input:', this.property.owner);
    
    // Check if the owner data needs to be mapped to IUserProfile structure
    if (this.property.owner) {
      console.log('Property Host Component - Owner keys:', Object.keys(this.property.owner));
      
      // Check if we have backend structure (with id/userName) that needs mapping to frontend structure (with userId/userName)
      const backendOwner = this.property.owner as any;
      if (backendOwner.id && !this.property.owner.userId) {
        console.log('Property Host Component - Mapping from backend structure to frontend IUserProfile');
        
        // Map the backend structure to frontend IUserProfile
        this.property.owner = {
          userId: backendOwner.id, // Backend id -> Frontend userId
          userName: backendOwner.userName, // Backend userName -> Frontend userName
          firstName: backendOwner.userName?.split(' ')[0] || 'Host',
          lastName: backendOwner.userName?.split(' ').slice(1).join(' ') || '',
          email: '',
          phoneNumber: '',
          bio: '',
          birthDate: '',
          status: 'Active',
          country: { id: 1, name: 'Unknown', regionId: 1 },
          photo: {
            id: 'default',
            photoUrl: 'logo.png',
          },
          roles: ['Host']
        };
        
        console.log('Property Host Component - Mapped owner:', this.property.owner);
      }
    }
  }

  get hostName(): string {
    return this.property.owner?.userName || 'Host';
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
    console.log('Contact Host clicked - Property:', this.property);
    console.log('Contact Host clicked - Owner:', this.property.owner);
    
    const currentUserId = this.tokenStorage.getUserId();
    const hostId = this.property.owner?.userId; // Use userId from IUserProfile interface
    
    console.log('Contact Host clicked - Current user ID:', currentUserId);
    console.log('Contact Host clicked - Host ID:', hostId);
    
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
    // Ensure current user is always user1Id for consistency
    this.chatService.startConversation({
      user1Id: currentUserId,
      user2Id: hostId,
      propertyId: this.property.id
    }).subscribe({
      next: (conversation) => {
        console.log('Conversation started successfully:', conversation);
        
        // Send initial message about the property
        const initialMessage = `Hi! I'm interested in your property "${this.property.title}". Is this unit available for booking? Could you tell me more about it?`;
        
        this.chatService.sendMessage({
          senderId: currentUserId,
          receiverId: hostId,
          content: initialMessage,
          conversationId: conversation.id
        }).subscribe({
          next: (message) => {
            console.log('Initial message sent successfully:', message);
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
        console.error('Error details:', {
          status: error.status,
          statusText: error.statusText,
          error: error.error,
          message: error.message
        });
        this.toastService.showError('Failed to start conversation with host');
      }
    });
  }
}