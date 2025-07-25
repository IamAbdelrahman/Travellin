import { Component, OnInit } from '@angular/core';
import { ToastService } from '../../services/toast.service';
import { UpgradeServiceService } from '../../services/upgrade-service.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-host-upgrade-aproval',
  imports: [CommonModule, FormsModule],
  templateUrl: './host-upgrade-aproval.component.html',
  styleUrl: './host-upgrade-aproval.component.css',
})
export class HostUpgradeAprovalComponent implements OnInit {
  requests: any[] = [];
  selectedRequest: any = null;
  showRejectModal = false;
  rejectionReason = '';
  requestToReject: any = null;
  enlargedImage: string | null = null;
  Math = Math;

  // Pagination
  currentPage = 1;
  pageSize = 10;
  totalItems = 0;

  // Filters
  statusFilter = '';
  documentTypeFilter = '';

  documentTypes = ['Passport', 'NationalId'];

  constructor(
    private hostUpgradeService: UpgradeServiceService,
    private toastr: ToastService
  ) {}

  ngOnInit(): void {
    console.log('Component initialized');
    this.loadRequests();
  }

  loadRequests(): void {
    const params = {
      page: this.currentPage,
      pageSize: this.pageSize,
      status: this.statusFilter,
      documentType: this.documentTypeFilter,
    };
    console.log('Sending params:', params);

    this.hostUpgradeService.getUpgradeRequests(params).subscribe({
      next: response => {
        console.log('API Response:', response);
        this.requests = response.items;
        this.totalItems = response.metaData.total;
        console.log('Updated requests:', this.requests);
      },
      error: err => {
        this.toastr.showError('Failed to load requests', 5000);
        console.error(err);
      },
    });
  }

  openDetailsModal(request: any): void {
    this.selectedRequest = request;
    this.showRejectModal = false;
  }

  closeModal(): void {
    this.selectedRequest = null;
  }

  openRejectModal(request: any): void {
    console.log('Opening reject modal for request:', request);
    this.selectedRequest = null;
    this.requestToReject = request;
    this.showRejectModal = true;
    console.log('showRejectModal set to:', this.showRejectModal);
  }

  closeRejectModal(): void {
    this.showRejectModal = false;
    this.requestToReject = null;
    this.rejectionReason = '';
  }

  approveRequest(requestId: string): void {
    this.hostUpgradeService.approveRequest(requestId).subscribe({
      next: () => {
        this.toastr.showSuccess('Request approved successfully');
        this.loadRequests();
        this.closeModal();
      },
      error: err => {
        this.toastr.showError('Failed to approve request', 5000);
        console.error(err);
      },
    });
  }

  rejectRequest(): void {
    // Validate rejection reason
    if (!this.rejectionReason || this.rejectionReason.trim().length === 0) {
      this.toastr.showWarning('Please provide a valid rejection reason');
      return;
    }

    // Show loading state
    const loadingToast = this.toastr.showInfo('Processing rejection...');

    this.hostUpgradeService
      .rejectRequest(this.requestToReject.id, this.rejectionReason)
      .subscribe({
        next: () => {
          // this.toastr.dismiss(loadingToast);
          this.toastr.showSuccess('Request rejected successfully');
          this.loadRequests();
          this.closeRejectModal();
          this.closeModal();
        },
        error: err => {
          this.toastr.showError(
            'An error occurred while processing the rejection',
            5000
          );

          if (err.status === 400) {
            this.toastr.showError('Invalid rejection reason format', 5000);
          } else {
            this.toastr.showError('Failed to reject request', 5000);
          }

          console.error('Rejection error:', err);
        },
      });
  }

  openImageModal(imageUrl: string): void {
    this.enlargedImage = imageUrl;
  }

  closeImageModal(): void {
    this.enlargedImage = null;
  }

  // Pagination methods
  getPageNumbers(): number[] {
    const totalPages = Math.ceil(this.totalItems / this.pageSize);
    return Array.from({ length: totalPages }, (_, i) => i + 1);
  }

  goToPage(page: number): void {
    this.currentPage = page;
    this.loadRequests();
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadRequests();
    }
  }

  nextPage(): void {
    if (this.currentPage * this.pageSize < this.totalItems) {
      this.currentPage++;
      this.loadRequests();
    }
  }

  get showingFrom(): number {
    return (this.currentPage - 1) * this.pageSize + 1;
  }

  get showingTo(): number {
    return Math.min(this.currentPage * this.pageSize, this.totalItems);
  }
}
