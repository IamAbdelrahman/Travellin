import { Directive, HostListener, Output, EventEmitter, ElementRef } from '@angular/core';

@Directive({
  selector: '[appDragDrop]',
  standalone: true
})
export class DragDropDirective {
  @Output() filesDropped = new EventEmitter<FileList>();

  constructor(private el: ElementRef) {}

  @HostListener('dragover', ['$event']) onDragOver(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    this.el.nativeElement.classList.add('drag-over');
  }

  @HostListener('dragleave', ['$event']) onDragLeave(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    this.el.nativeElement.classList.remove('drag-over');
  }

  @HostListener('drop', ['$event']) onDrop(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    this.el.nativeElement.classList.remove('drag-over');
    
    if (event.dataTransfer?.files && event.dataTransfer.files.length > 0) {
      this.filesDropped.emit(event.dataTransfer.files);
    }
  }
}