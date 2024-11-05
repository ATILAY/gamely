import { Game } from './game.model';

export interface Publisher {
  id: string;
  name: string;
  country?: string;
  websiteUrl?: string;
  games?: Game[];
  createdAt: Date;
  updatedAt?: Date;
}
