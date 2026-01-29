import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Router, NavigationEnd } from '@angular/router';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { Component } from '@angular/core';
import { filter } from 'rxjs/operators';
@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [RouterOutlet,
    RouterLink,
    RouterLinkActive,
    MatSidenavModule,
    MatToolbarModule,
    MatListModule,
    MatIconModule
  ],
  templateUrl: './layout.html',
  styleUrl: './layout.css',
})
export class Layout {
  currentTitle = 'TeamTasks Dashboard';

  private titles: Record<string, string> = {
    '/dashboard': 'Métricas',
    '/developers': 'Desarrolladores',
    '/projects': 'Proyectos'
  };

  constructor(private router: Router) {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: any) => {
        this.currentTitle = this.titles[event.urlAfterRedirects] || 'TeamTasks Dashboard';
      });
  }
}
