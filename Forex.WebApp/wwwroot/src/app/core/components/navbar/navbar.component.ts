import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  public menu: MenuItem[]; 

  constructor(private router: Router) {     
  }

  ngOnInit(): void {
    this.buildMenu();
  }

  private buildMenu() {
    this.menu = [
      {label: 'Currency Exchange', command: () => this.routeToCurrencyExchange()},
      {label: 'Rate History', command: () => this.routeToRateHistory()}
    ];
  }

  private routeToCurrencyExchange() {
    this.router.navigate(['currency-exchange']);
  }

  private routeToRateHistory() {
    this.router.navigate(['rate-change-history']);
  }

}
