import { Publisher } from './publisher.model';
import { Review } from './review.model';
import { Platform } from './platform.model';

export interface Game {
  id: string;
  title: string;
  description?: string;
  releaseDate?: Date;
  publisherId?: string;
  publisher?: Publisher;
  reviews?: Review[];
  platforms?: Platform[];
  genre?: string;
  averageRating?: number;
  coverImageUrl?: string;
  createdAt: Date;
  updatedAt?: Date;
}
