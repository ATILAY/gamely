import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Game } from '../../../models/game.model'; // import your Game model
import { environment } from '../../../../../environments/environment';
import { HttpHeaders } from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  }),
};

@Injectable({
  providedIn: 'root',
})
export class GameService {
  private dummyUrl = 'https://localhost:7091/api';
  private apiUrl = `${this.dummyUrl}/Games`; // `${environment.apiUrl}/Games`;

  constructor(private http: HttpClient) {}

  // Method to add a new game (POST request)
  addGame(game: Game): Observable<Game> {
    const dummyPayload = {
      title: 'qweqwe121312qweqwe',
      description: 'string',
      releaseDate: '2024-11-04T20:09:36.120Z',
      genre: 'string',
      averageRating: 0,
      coverImageUrl: 'string',
      createdAt: '2024-11-04T20:09:36.120Z',
      updatedAt: '2024-11-04T20:09:36.120Z',

      // publisherId: 'publisherIdDummy',
    };

    console.log('dummyPayload:', dummyPayload);

    const payload = {
      ...game,
      averageRating: game.averageRating || 0,
      releaseDate: game.releaseDate || new Date().toISOString(),
      createdAt: game.createdAt || new Date().toISOString(),
      updatedAt: game.updatedAt || new Date().toISOString(),
    };

    console.log('payload', payload);
    // return this.http.post<Game>(this.apiUrl, game, httpOptions);
    return this.http.post<Game>(this.apiUrl, payload, httpOptions);
    // return this.http.post<Game>(this.apiUrl, dummyPayload, httpOptions);
  }

  // Method to update an existing game (PUT request)
  updateGame(gameId: string, game: Game): Observable<Game> {
    // return this.http.put<Game>(`${this.apiUrl}/${gameId}`, game);
    return this.http.put<Game>(`${this.apiUrl}/${gameId}`, game);
  }

  // Method to get a game by ID (GET request)
  getGameById(gameId: string): Observable<Game> {
    return this.http.get<Game>(`${this.apiUrl}/${gameId}`);
  }
}
