import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '../../../helper/ValidatorField';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.css']
})
export class CadastroComponent implements OnInit {

  form!: FormGroup
  constructor(private formBuilder: FormBuilder) { }

  public criarIntancia(): void {

    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('senha', 'confirmarSenha')
    };

    this.form = this.formBuilder.group({

      primeiroNome:   ['', Validators.required],
      ultimoNome:     ['', Validators.required],
      email:          ['', [Validators.required, Validators.email]],
      usuario:        ['', Validators.required],
      senha:          ['', [Validators.required,Validators.minLength(6)]],
      confirmarSenha: ['', Validators.required],

    }, formOptions)
  }

  public get primeiroNome(): any {
    return this.form.get('primeiroNome');
  }

  public get ultimoNome(): any {
    return this.form.get('ultimoNome');
  }

  public get email(): any {
    return this.form.get('email');
  }

  public get usuario(): any {
    return this.form.get('usuario');
  }

  public get senha(): any {
    return this.form.get('senha');
  }

  public get confirmarSenha(): any {
    return this.form.get('confirmarSenha');
  }

  ngOnInit(): void {
    this.criarIntancia();
  }

}
