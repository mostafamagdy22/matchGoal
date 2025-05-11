import { AbstractControl, AsyncValidatorFn, ValidationErrors } from '@angular/forms';
import { AuthService } from '../Services/AuthService/auth.service';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

export function emailExistsValidator(authService: AuthService): AsyncValidatorFn {
  return (control: AbstractControl): Observable<ValidationErrors | null> => {
    if (!control.value) return of(null); // skip if empty

    return authService.checkEmailExists(control.value).pipe(
      map(exists => (exists ? { emailExists: true } : null)),
      catchError(() => of(null)) // treat errors as valid
    );
  };
}
