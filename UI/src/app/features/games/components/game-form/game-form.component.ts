import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import {
  RouterOutlet,
  RouterLink,
  RouterLinkActive,
  Router,
} from '@angular/router';
import { Game } from '../../../../core/models/game.model';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { GameService } from '../../../../core/services/api/game-service/game.service';
import { v4 as uuidv4 } from 'uuid';

@Component({
  selector: 'app-game-form',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, ReactiveFormsModule, CommonModule],
  templateUrl: './game-form.component.html',
  styleUrl: './game-form.component.css',
})
export class GameFormComponent implements OnInit {
  gameForm: FormGroup;
  isEditing: boolean = false;

  gameId: string | null | undefined;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private gameService: GameService
  ) {
    this.gameForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      releaseDate: [''],
      publisherId: [uuidv4(), Validators.required],
      genre: [''],
      averageRating: [''],
      coverImageUrl: [''],
    });
  }

  ngOnInit(): void {
    // Disable the 'publisherId' field
    this.gameForm.get('publisherId')?.disable();
    // Check if this is an edit form based on the route parameter
    this.route.paramMap.subscribe((params) => {
      this.gameId = params.get('id');
      this.isEditing = !!this?.gameId;

      if (this.isEditing && this?.gameId) {
        this.loadGameDetails(this?.gameId);
      }
    });
  }

  loadGameDetails(gameId: string): void {
    // Implement a method to fetch the game details and populate the form if editing
    // Assume there's a method getGameById in the GameService
    this.gameService.getGameById(gameId).subscribe((game: Game) => {
      this.gameForm.patchValue(game);
    });
  }

  onSubmit(): void {
    if (this.gameForm.valid) {
      const game: Game = this.gameForm.value;
      console.log('Form Submit Action:', game);
      // Add your logic to handle the form submission, e.g., call a service to save the game

      if (this.isEditing && this.gameId) {
        // Update the game if editing
        this.gameService.updateGame(this.gameId, game).subscribe({
          next: () => {
            console.log('Game updated successfully');
            this.router.navigate(['/games']); // Navigate to a different route after success
          },
          error: (err: any) => console.error('Error updating game:', err),
        });
      } else {
        // Add a new game if not editing
        this.gameService.addGame(game).subscribe({
          next: () => {
            console.log('Game added successfully');
            this.router.navigate(['/game-list']);
          },
          error: (err: any) => console.error('Error adding game:', err),
        });
      }
    }
  }

  populateForm(game: Game): void {
    this.gameForm.patchValue(game);
  }
}
