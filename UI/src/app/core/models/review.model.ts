import { Game } from './game.model';

export interface Review {
  id: string;
  rating: number;
  content?: string;
  reviewer?: string;
  gameId: string;
  game?: Game;
  createdAt: Date;
}
