import { FileUploadRequest } from './file-upload-request';

export interface UpgradeHostRequest {
  Id?: string;
  UserId?: string;
  Status?: string;
  ApprovedBy?: string;
  ApprovalDate?: string;
  RejectionReason?: string;
  DocumentType: DocumentType;
  DocumentNumber: string;
  FirstName: string;
  LastName: string;
  ExpiryDate: string;
  FrontPhoto: FileUploadRequest;
  BackPhoto: FileUploadRequest;
  CreatedAt?: Date;
  UpdatedAt?: Date;
}
