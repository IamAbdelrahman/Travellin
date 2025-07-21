import { Directive, EventEmitter, Output, HostListener } from '@angular/core';

@Directive({
  selector: '[appDragDrop]',
  standalone: true  // Add this for standalone component compatibility
})
export class DragDropDirective {
  @Output() filesDropped = new EventEmitter<FileList>();

  // Prevent default drag behaviors
  @HostListener('dragover', ['$event'])
  onDragOver(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
  }

  @HostListener('dragenter', ['$event'])  // Add dragenter for better UX
  onDragEnter(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
  }

  @HostListener('dragleave', ['$event'])
  onDragLeave(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
  }

  // Handle dropped files
  @HostListener('drop', ['$event'])
  onDrop(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();

    const files = event.dataTransfer?.files;
    if (files && files.length > 0) {
      this.filesDropped.emit(files);
    }
  }
}
