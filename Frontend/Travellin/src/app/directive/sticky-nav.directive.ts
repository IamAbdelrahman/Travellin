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
  private lastScrollTop = 0;
  private scrollThreshold = 100; // Minimum scroll before hiding

  constructor(
    private el: ElementRef,
    private renderer: Renderer2
  ) {}

  ngOnInit() {
    // Apply smooth transitions for background and transform
    this.renderer.setStyle(
      this.el.nativeElement,
      'transition',
      'background-color 0.3s ease-in-out, transform 0.3s ease-in-out, box-shadow 0.3s ease-in-out'
    );
    
    // Add initial styles
    this.renderer.setStyle(this.el.nativeElement, 'position', 'fixed');
    this.renderer.setStyle(this.el.nativeElement, 'top', '0');
    this.renderer.setStyle(this.el.nativeElement, 'left', '0');
    this.renderer.setStyle(this.el.nativeElement, 'right', '0');
    this.renderer.setStyle(this.el.nativeElement, 'z-index', '1000');
  }

  @HostListener('window:scroll', [])
  onWindowScroll() {
    const currentScrollTop = window.pageYOffset;
    const scrollDelta = currentScrollTop - this.lastScrollTop;
    
    // Add sticky class for background and shadow
    if (currentScrollTop > 0) {
      this.renderer.addClass(this.el.nativeElement, 'nav-sticky');
    } else {
      this.renderer.removeClass(this.el.nativeElement, 'nav-sticky');
    }
    
    // Enhanced scroll behavior - only hide on significant scroll down
    if (currentScrollTop > this.scrollThreshold && scrollDelta > 10) {
      // Scrolling down significantly - hide header
      this.renderer.setStyle(this.el.nativeElement, 'transform', 'translateY(-100%)');
    } else if (scrollDelta < -5 || currentScrollTop < this.scrollThreshold) {
      // Scrolling up or near top - show header
      this.renderer.setStyle(this.el.nativeElement, 'transform', 'translateY(0)');
    }
    
    this.lastScrollTop = currentScrollTop;
  }

  @HostListener('window:resize', [])
  onWindowResize() {
    // Ensure header is visible when window is resized
    this.renderer.setStyle(this.el.nativeElement, 'transform', 'translateY(0)');
  }
}
