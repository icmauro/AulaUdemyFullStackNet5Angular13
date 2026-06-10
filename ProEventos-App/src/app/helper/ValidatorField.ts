import { AbstractControl } from "@angular/forms";

export class ValidatorField {

  static MustMatch(controlName: string, matchingControlName: string): any {

    return (formGroup: AbstractControl) => {
      const control = formGroup.get(controlName);
      const matchingControl = formGroup.get(matchingControlName);

      if (matchingControl?.errors && !matchingControl.errors['mustMatch']) {
        return null;
      }

      if (control?.value !== matchingControl?.value) {
        matchingControl?.setErrors({ mustMatch: true });
      }
      else {
        matchingControl?.setErrors(null);
      }

      return null;
    }

  }

}
