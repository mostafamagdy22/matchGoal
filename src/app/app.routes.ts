import { Routes } from '@angular/router';
import { MatchListComponent } from './match-list/match-list.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AddMatchComponent } from './add-match/add-match.component';
import { UpdatematchComponent } from './update-match/update-match.component';
import { PlayListComponent } from './play-list/play-list.component';

export const routes: Routes =
[
  { path: '', component: MatchListComponent },
  { path: 'matches',component: MatchListComponent},
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'matches/addmatch', component: AddMatchComponent },
  { path: 'matches/updatematch/:id', component: UpdatematchComponent },
  { path: 'playlist', component: PlayListComponent }
];
