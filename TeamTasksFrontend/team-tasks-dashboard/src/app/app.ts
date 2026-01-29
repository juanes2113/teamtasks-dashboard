import { Component } from '@angular/core';
import { Layout } from './core/layout/layout/layout';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [Layout],
  template: `<app-layout></app-layout>`
})
export class App { }

/*import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('team-tasks-dashboard');
}*/
