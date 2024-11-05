import { Game } from './game.model';

export interface Platform {
  id: string;
  name: string;
  games?: Game[];
  createdAt: Date;
  updatedAt?: Date;
}
