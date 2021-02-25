import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AppconfigService } from './core/services/appconfig.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'FOREX';

  constructor(private titleService: Title) {
    this.titleService.setTitle(this.title);        
  }
}
