export type DocumentType = 'Passport' | 'NationalId';

export interface HostUpgradeFormDto {
  DocumentType: DocumentType;
  DocumentNumber: string;
  FirstName: string;
  LastName: string;
  ExpiryDate: string;
  FrontPhoto: File;
  BackPhoto: File;
}
