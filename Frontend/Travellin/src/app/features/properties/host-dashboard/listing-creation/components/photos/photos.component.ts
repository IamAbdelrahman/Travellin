import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DragDropDirective } from '../../shared/directives/drag-drop';

@Component({
  selector: 'app-photos',
  standalone: true,
  imports: [CommonModule, DragDropDirective],
  templateUrl: './photos.component.html',
  styleUrls: ['./photos.component.css']
})
export class PhotosComponent {
  photos: { file: File, url: string }[] = [];

  removePhoto(index: number): void {
    this.photos.splice(index, 1);
  }

  onFilesSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.handleFiles(input.files);
      input.value = '';
    }
  }

  // This method now correctly receives FileList from the directive
  onFilesDropped(files: FileList): void {
    this.handleFiles(files);
  }

  private handleFiles(files: FileList): void {
    for (let i = 0; i < files.length; i++) {
      const file = files[i];
      if (file.type.match('image.*')) {
        const reader = new FileReader();
        reader.onload = (e: ProgressEvent<FileReader>) => {
          if (e.target?.result) {
            this.photos.push({
              file,
              url: e.target.result as string
            });
          }
        };
        reader.readAsDataURL(file);
      }
    }
  }
}
