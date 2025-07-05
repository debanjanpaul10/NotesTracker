import { Component } from '@angular/core';
import { ToastModule } from 'primeng/toast';

@Component({
  selector: 'app-toaster',
  standalone: true,
  imports: [ToastModule],
  templateUrl: './toaster.component.html',
  styleUrls: ['./toaster.component.scss'],
})
class ToasterComponent {}

export { ToasterComponent };
