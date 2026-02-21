import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrls: ['./app.css'],
  standalone: false
})
export class AppComponent {

  sidebarVisible: boolean = false;

  constructor(private router: Router) {
    // navegação para fechar o Drawer/Sidebar automaticamente
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      this.sidebarVisible = false; 
    });
  }
}
