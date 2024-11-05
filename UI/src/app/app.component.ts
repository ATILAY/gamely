import { HttpClient, provideHttpClient, withFetch } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Contact } from '../models/contact.model';
import { AsyncPipe } from '@angular/common';
import { NgbAlert } from '@ng-bootstrap/ng-bootstrap';
import { GameFormComponent } from './features/games/components/game-form/game-form.component';
import { GameListComponent } from './features/games/components/game-list/game-list.component';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';

import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    AsyncPipe,
    NgbAlert,
    GameFormComponent,
    RouterLink,
    RouterLinkActive,
    GameListComponent,
    ReactiveFormsModule,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'contactly.web';
  http = inject(HttpClient);

  contacts$ = this.getContacts();

  private getContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>('https://localhost:7091/api/Contacts');
  }
}
