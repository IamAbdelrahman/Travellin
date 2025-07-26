import {
  Directive,
  ElementRef,
  HostListener,
  Renderer2,
  OnInit,
} from '@angular/core';

@Directive({
  selector: '[appStickyNav]',
  standalone: true,
})
export class StickyNavDirective implements OnInit {
  constructor(
    private el: ElementRef,
    private renderer: Renderer2
  ) {}

  ngOnInit() {
    // Apply separate transitions for background and shadow
    this.renderer.setStyle(
      this.el.nativeElement,
      'transition',
      'background-color 0.2s ease-in-out, box-shadow 0.5s ease-in-out'
    );
  }

  @HostListener('window:scroll', [])
  onWindowScroll() {
    if (window.pageYOffset > 0) {
      this.renderer.addClass(this.el.nativeElement, 'nav-sticky');
    } else {
      this.renderer.removeClass(this.el.nativeElement, 'nav-sticky');
    }
  }
}
