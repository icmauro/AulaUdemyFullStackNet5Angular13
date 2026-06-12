import { FormControlName } from "@angular/forms";

export class Constants {

  static readonly DATE_TIME_FORMAT = 'DD/MM/YYYY';
  static readonly DATE_FORMAT = 'dd/MM/yyyy';
  static readonly LOCALE = 'pt-BR';
  static readonly BRASILIAN_CURRENCY = 'BRL';
  static readonly DATE_TIME_FORMAT_PICKER = {
    isAnimated: true,
    adaptivePosition: true,
    dateInputFormat: 'DD/MM/YYYY hh:mm a',
    containerClass: 'theme-default',
    showWeekNumbers: false
  };

  static cssValidatorForm(formControl: FormControlName): any {
    return { 'is-invalid': formControl?.errors && formControl?.touched }
  };
} 
